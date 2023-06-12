using Gss.CorporateApps.Core;
using Gss.CorporateApps.Infrastructure.Contracts.OperationResult;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using PolicyOperation.Core.Managers;
using PolicyOperation.Data.Repositories;
using PolicyOperation.ExternalServices.Interface;
using PolicyOperation.ExternalServices.Service;
using PolicyOperation.Models.Entidad;
using PolicyOperation.Models.ExternalEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace PolicyOperation.Core.Entidad.Policy
{
    public class PoliciesSummaryHandler : BaseHandler<PoliciesSummaryRequest>
    {
        private IMemoryCache _cacheProvider;
        private ITimeRepository _timeRepository;
        private IIntermediariesForUser _intermediariesForUser;
        private IPoliciesSummary _policiesSummary;
        public PoliciesSummaryHandler(IMemoryCache cacheProvider, ITimeRepository timeRepository, IIntermediariesForUser intermediariesForUser, IPoliciesSummary policiesSummary)
        {
            _cacheProvider = cacheProvider;
            _timeRepository = timeRepository;
            _intermediariesForUser = intermediariesForUser;
            _policiesSummary = policiesSummary;


            if (!_cacheProvider.TryGetValue("_CeiboUserModel", out List<CeiboUserModel> usermodels))
            {
                List<CeiboUserModel> ceiboUserModel = new List<CeiboUserModel>();
                _cacheProvider.Set<List<CeiboUserModel>>("_CeiboUserModel", ceiboUserModel);
            };
        }

        protected override async Task<IOperationResult> ExecuteAsync(PoliciesSummaryRequest request, CancellationToken cancellationToken)
        {
            // recupero el modelo de usuario de cache 
            CeiboUserModel userModel = null;
            UserCaching userCaching = new UserCaching(_timeRepository, _cacheProvider);
            userModel = await userCaching.GetuserFromCache(request.token) as CeiboUserModel;

            //Recupero la lista de intermediarios del usuario
            IntermediariesUserDTO intermediariesList = await _intermediariesForUser.GetIntermediariesForUser(userModel, request.token);

            //Invoco al servicio de backend
            PoliciesSummaryRequestDTO dto = GetPoliciesSummaryRequestDTO(request, intermediariesList);
            PoliciesSummaryResponseDTO policiesSummaryResponseDTO = await _policiesSummary.GetPoliciesSummaries(dto, userModel);
            //Mapeo el DTO al Modelo
            PoliciesSummaryModel model = GetPoliciesSummary(policiesSummaryResponseDTO);

            return new SuccessResult(model);
        }


        private  PoliciesSummaryRequestDTO GetPoliciesSummaryRequestDTO (PoliciesSummaryRequest request, IntermediariesUserDTO intermediariesList)
        {

            try
            {
                string listIntermediaries = String.Join(",", intermediariesList.Intermediaries.ToArray());
                //string joinedNames = String.Join(", ", names.ToArray());

                PoliciesSummaryRequestDTO dto = new PoliciesSummaryRequestDTO
                {
                    policy = new PolicyDTO
                    {
                        branchCode = request.policySearch.branchCode != null ? request.policySearch.branchCode:null ,
                        personRoleCode = request.policySearch.personRoleCode != null? request.policySearch.personRoleCode: null,
                        //certificateNumber = int.Parse(request.policySearch.certificateNumber),
                        officialNumber = request.policySearch.officialNumber != null ? request.policySearch.officialNumber : null,
                        productCode = request.policySearch.productCode != null? request.policySearch.productCode:null,
                        referenceNumber = request.policySearch.referenceNumber !=null? request.policySearch.referenceNumber:null,
                        startDate = request.policySearch.startEffectiveDate != null? request.policySearch.startEffectiveDate : null,
                        endDate = request.policySearch.endEffectiveDate != null ?request.policySearch.endEffectiveDate :null,
                        client = new Models.ExternalEntities.Client
                        {
                            bupId = request.policySearch.client.bupId,
                            firstName = request.policySearch.client.firstName != null? request.policySearch.client.firstName: null,
                            lastName = request.policySearch.client.lastName != null? request.policySearch.client.lastName:null,

                        },
                        producerCode = listIntermediaries,
                    }
                };


                return dto;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }


        private PoliciesSummaryModel GetPoliciesSummary(PoliciesSummaryResponseDTO dto)
        {
            PoliciesSummaryModel model = null;

            List<MessageModel> messages = new List<MessageModel>();


            foreach (var message in dto.messages)
            {
                MessageModel mjs = new MessageModel()
                {
                    code = message.code,
                    help = message.help,
                    status = message.status,
                    text = message.text
                };

                messages.Add(mjs);

            }
            model = new PoliciesSummaryModel();
            model.messages = messages;

            if(dto.policies != null) {

                List <Policieslist> policieslist = new List<Policieslist>();

                //Obtengo la URL para armar el parametro de MEta
                try { 
                var builder = new ConfigurationBuilder()
                .AddJsonFile($"Config/urlHost.json", optional: false, reloadOnChange: true);


                var configuration = builder.Build();

                string url = configuration["HostName:UrlExpose"];


                    foreach (var policy in dto.policies)
                    {
                        string referenceNumber = (policy.coreId == 1) ? policy.certificateId.ToString() : policy.referenceNumber;
                        string puid = $"{policy.bupId}-" +
                            $"{policy.branchCode}-" +
                            $"{policy.productCode}-" +
                            $"{referenceNumber}-" +
                            $"{policy.certificateNumber}-" +
                            $"{policy.coreId}";


                        var puidB64 = Encoding.UTF8.GetBytes(puid);
                        string puidbase64String = Convert.ToBase64String(puidB64);//, Base64FormattingOptions.InsertLineBreaks);

                        Policieslist policiesList = new Policieslist
                        {
                            branchCode = policy.branchCode,
                            branchName = policy.branchName,
                            bupId = policy.bupId,
                            certificateNumber = policy.certificateNumber,
                            endEffectiveDate = policy.endDate,
                            startEffectiveDate = policy.startDate,
                            policyGroupTypeCode = policy.policyGroupTypeCode,
                            policyGroupTypeName = policy.policyGroupTypeName,
                            policyStatusCode = policy.policyStatusCode,
                            policyTypeCode = policy.policyTypeCode,
                            policyTypeName = policy.policyTypeName,
                            productCode = policy.productCode,
                            productName = policy.productName,
                            referenceNumber = referenceNumber,
                            puid = puidbase64String,
                            meta = new MetaModel
                            {
                                details = $"{url}/policies/{puidbase64String}"
                            }

                        };
                        policieslist.Add(policiesList);

                    }
                    

                }
                catch (Exception ex) 
                { 
                    Console.WriteLine(ex.Message); 
                }
                model.policiesList = policieslist;

            }


            return model;
        }


    }
}
