using AutoMapper;
using Azure.Core;
using Gss.CorporateApps.Core;
using Gss.CorporateApps.Infrastructure.Contracts.OperationResult;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using PolicyOperation.Core.Managers;
using PolicyOperation.Data.Repositories;
using PolicyOperation.Data.TimePro;
using PolicyOperation.ExternalServices.Interface;
using PolicyOperation.Models.Entidad;
using PolicyOperation.Models.ExternalEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PolicyOperation.Core.Entidad.Policy
{
    public class PolicyHandler : BaseHandler<PolicyRequest>
    {
        private readonly IPolicyForBupIdValidation _policyForBupIdValidation;
        private readonly IPolicyCertificateDatail _policyCertificateDatail;
        private PuidModel _puid;
        private IMemoryCache _cacheProvider;
        private readonly IProfileUserData _profileUserData;
        private readonly ITimeRepository _timeRepository;


        public PolicyHandler(IPolicyForBupIdValidation policyForBupIdValidation, IPolicyCertificateDatail policyCertificateDatail, IMemoryCache cacheProvider, IProfileUserData profileUserData, ITimeRepository timeRepository)
        {
            _policyForBupIdValidation = policyForBupIdValidation;
            _policyCertificateDatail = policyCertificateDatail;
            _cacheProvider = cacheProvider;
            _profileUserData = profileUserData;
            _timeRepository = timeRepository;


            if (!_cacheProvider.TryGetValue("_CeiboUserModel", out List<CeiboUserModel> usermodels))
            {
                List<CeiboUserModel> ceiboUserModel = new List<CeiboUserModel>();
                _cacheProvider.Set<List<CeiboUserModel>>("_CeiboUserModel", ceiboUserModel);
            };   
        }

        protected override async Task<IOperationResult> ExecuteAsync(PolicyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // ... 
                Console.WriteLine(request.puid);
                _puid = new PuidModel(request.puid);

                CeiboUserModel userModel = null;
                UserCaching userCaching = new UserCaching(_timeRepository, _cacheProvider);
                userModel = await userCaching.GetuserFromCache(request.token) as CeiboUserModel;

                //Flujo segun COreId
                if (_puid.coreId == 1)
                {
                    PolicyModel model = await this.PolicyOfTime(request.token, _puid, userModel);
                    return new SuccessResult(model);

                    //metodo Time
                }
                else if (_puid.coreId == 2)
                {
                    PolicyModel model = await this.PolicyOfGuideWire(request.token, _puid, userModel);
                    return new SuccessResult(model);
                    //metodo GW
                }
                else
                {
                    return new SuccessResult();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ExceptionModel model = new ExceptionModel
                {
                    messages = new List<MessageModel> { new MessageModel {
                        code = "GSS-500-000",
                        help = ex.Message.ToString(),
                        status = "500",
                        text = ex.Message.ToString()
                        }
                    },
                    httpStatusCode = 500
                };
                return new SuccessResult(model);
            }


        }

        private List<CeiboUserModel> CacheCeiboUserModel()
        {
            

            if (!_cacheProvider.TryGetValue("_CeiboUserModel", out List<CeiboUserModel> usermodels))
            {
                usermodels = new List<CeiboUserModel>(); // Hacer
                //usermodels.Add(modelP);
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2),
                    Size = 1024,
                };
                _cacheProvider.Set("_CeiboUserModel", usermodels, cacheEntryOptions);
                return usermodels;
            }
            else
                return usermodels;

            
        }

        private void setCeiboUserModelCache(string userName, CeiboUserModel userModel)
        {

            List<CeiboUserModel> usermodels = _cacheProvider.Get("_CeiboUserModel") as List<CeiboUserModel>;
            usermodels.Add(userModel);
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Size = 1024,
            };
            _cacheProvider.Set("_CeiboUserModel", usermodels, cacheEntryOptions);

        }

        private string DecodePuid(string puid)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(puid);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

        }

        /*
         * Metodo para procesar el flujo de busqueda de poliza para Time
         */
        private async Task<PolicyModel> PolicyOfTime(string token, PuidModel puid, CeiboUserModel userModel)
        {
            //Primer paso, valido que la poliza pertenezca al cliente
            var validation = await _policyForBupIdValidation.ValidateclientcertificatesAsync(puid.bupID, Int32.Parse(puid.certificateId), token);

            PolicyModel model = null;
            if (validation.validateClientCertificate.success == true)
            {
                model = new PolicyModel();
                var result = await _policyCertificateDatail.GetPolicyDetails(puid, token);


                //var mapper = MapperConfig.InitializeAutomapper(puid);
                //model = mapper.Map<PolicyModel>(result);
                model = GetPolicymodeByDTO(result);
                return model;
 
            }
            else 
            {
                model = new PolicyModel();
                List<Message> messages = new List<Message>();

                Message mjs = new Message()
                    {
                        code = "GSS-401-000",
                        help = "",
                        status = "402",
                        text = "Poliza no corresponde al cliente"
                };

                messages.Add(mjs);

                model.messages = messages;
                return model;
            }
            
        }

        /*
         * Metodo para procesar el flujo de busqueda de poliza para GW
         */
        private async Task<PolicyModel> PolicyOfGuideWire(string token, PuidModel puid, CeiboUserModel userModel)
        {
            PolicyModel model = null;
            model = new PolicyModel();

            //Servicio de backEnd de intermediarios
            //var profileUser = await _profileUserData.GetData(token);
            //Busco el BupId por BD

            /*
            string userName = GetUserFromToken(token);
            UserQueryParamsReqModel usqPmodel = new UserQueryParamsReqModel() { UserName = userName };
            var resultQuery = await _timeRepository.QueryAsync(new GetDataUserQuery(usqPmodel));
            UserQueryParamsModel userModel = ((IEnumerable<UserQueryParamsModel>)resultQuery).FirstOrDefault();
            */
            PolicyCertificateDatailDTO result = await _policyCertificateDatail.GetPolicyDetailsGW(puid, token, userModel);

            if (result != null) {
                model = GetPolicymodeByDTO(result);
            }
            
            return model;

        }


        private PolicyModel GetPolicymodeByDTO(PolicyCertificateDatailDTO dto)
        {
            PolicyModel model = null;
            List<Message> messages = new List<Message>();

            
            foreach (var message in dto.messages)
            {
                Message mjs = new Message()
                {
                    code = message.code,
                    help = message.help,
                    status = message.status,
                    text = message.text
                };

                messages.Add(mjs);
                        
            }
            model = new PolicyModel();
            model.messages = messages;
            if (dto.certificateDetail != null)
            {
                
                {
                    model.policyDetails = new Policydetails
                    {
                        referenceNumber = 234234, //ver de donde sale
                        certificateNumber = 3,
                        branchCode = dto.certificateDetail.branchCode,
                        branchName = dto.certificateDetail.branchName,
                        productCode = 5,//"ver de donde sale"
                        productName = "ver de donde sale",
                        officialNumber = dto.certificateDetail.officialNumber,
                        startEffectiveDate = dto.certificateDetail.policyPeriodStartEffectiveDate,
                        endEffectiveDate = dto.certificateDetail.policyPeriodEndEffectiveDate,
                        policyTypeCode = dto.certificateDetail.policyTypeCode,
                        policyTypeName = dto.certificateDetail.policyTypeName,
                        conditionCode = dto.certificateDetail.conditionCode,
                        stateName = dto.certificateDetail.stateName,
                        coveredItemPremiun = dto.certificateDetail.coveredItemPremiun,
                        policyPeriodPremiun = dto.certificateDetail.policyPeriodPremiun,
                        chargePercentageQuantity = dto.certificateDetail.chargePercentageQuantity,
                        discountPercentage = dto.certificateDetail.discountPercentage,
                        email = dto.certificateDetail.email,
                        meta = new Models.Entidad.Meta
                        {
                            client = dto.certificateDetail.meta.client,
                            intermediaries = dto.certificateDetail.meta.intermediaries,
                            payment = dto.certificateDetail.meta.payment,
                            insuredRisks = dto.certificateDetail.meta.risk,
                            additionalGoods = dto.certificateDetail.meta.additionalGoods,
                            additionalCoverages = dto.certificateDetail.meta.additionalData,
                            coverages = dto.certificateDetail.meta.coverages,

                        }

                    };

                };
            }
            

            return model;
        }

        private static string GetUserFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var sub = tokenS.Claims.First(claim => claim.Type == "nickname").Value;

            string usercode = sub.Substring(sub.IndexOf("|") + 1);
            return usercode;
        }
    }
}

/*public class MapperConfig
{
    public static Mapper InitializeAutomapper( PuidModel puid)
    {
        //Provide all the Mapping Configuration
        var config = new MapperConfiguration(cfg =>
        {

            //Configuring PolicyCertificateDatailDTO and PolicyModel
            cfg.CreateMap<PolicyCertificateDatailDTO, PolicyModel>()
            .ForMember(dest => dest.policyDetails.referenceNumber, act => act.MapFrom(src => src.certificateDetail.officialNumber)) //ver de donde obtenero
            .ForMember(dest => dest.policyDetails.branchCode, act => act.MapFrom(src => src.certificateDetail.branchName))
            .ForMember(dest => dest.policyDetails.branchName, act => act.MapFrom(src => src.certificateDetail.branchCode));
            





            //Any Other Mapping Configuration ....
        });
        //Create an Instance of Mapper and return that Instance
        var mapper = new Mapper(config);
        return mapper;
    }
}*/

