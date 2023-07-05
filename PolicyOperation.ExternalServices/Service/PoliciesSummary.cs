using PolicyOperation.ExternalServices.Interface;
using PolicyOperation.Models.ExternalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PolicyOperation.Models.Entidad;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Policy;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Threading;

namespace PolicyOperation.ExternalServices.Service
{
    public class PoliciesSummary : IPoliciesSummary
    {

        private string _token;
        private EndpointAddress endpointAddress;

        public PoliciesSummary( EndpointAddress endpointAddress)
        {
            //this._token = token;
            this.endpointAddress = endpointAddress;
        }

             
        public async Task<PoliciesSummaryResponseDTO> GetPoliciesSummaries(PoliciesSummaryRequestDTO dto, CeiboUserModel userModel)
        {
            Log.Information("Metodo GetPoliciesSummaries");
            try { 
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);

                    //client.DefaultRequestHeaders.Add("CoreId", puid.coreId.ToString());
                    client.DefaultRequestHeaders.Add("ApplicationId", "14");
                    client.DefaultRequestHeaders.Add("CompanyCode", "1");
                    client.DefaultRequestHeaders.Add("ClientTypeId", "1");

                    ESBTokenModel tokenModel = new ESBTokenModel();
                    tokenModel.userName = userModel.UserCode.ToString();
                    tokenModel.bupId = userModel.BupID;
                    string output = JsonConvert.SerializeObject(tokenModel);

                    var tktESBAuthorization = Encoding.UTF8.GetBytes(output);
                    string tktESBbase64String = Convert.ToBase64String(tktESBAuthorization).TrimEnd('=');//, Base64FormattingOptions.InsertLineBreaks);


                    //client.DefaultRequestHeaders.Add("Authorization", tktESBbase64String);
                    client.DefaultRequestHeaders.Add("Authorization", "ewogICJ1c2VyTmFtZSI6ICIzMjQ0NCIsCiAgImJ1cElkIjogNTI2MTMyNwp9");




                    string endpoint = "";
                           endpoint = $"{endpointAddress}" ;

          
                    var json = JsonConvert.SerializeObject(dto, Newtonsoft.Json.Formatting.None,
                                    new JsonSerializerSettings
                                    {
                                        NullValueHandling = NullValueHandling.Ignore
                                    });
                        var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

                    Log.Information("Invocaion BackEnd: " + endpoint);
                    Log.Information("Request : " + json);
                    Log.Information("Token : " + tktESBbase64String);
                    

                    using (var Response = await client.PostAsync(endpoint, stringContent))
                        {
                            var result = await Response.Content.ReadAsStringAsync();
                            var response = JsonConvert.DeserializeObject<Models.ExternalEntities.PoliciesSummaryResponseDTO>(result);
                            Log.Information("Response :" + result);
                            return response;
                          }
               
                    }
                }
            catch (TaskCanceledException to)
            {
                Log.Information("Error GetPoliciesSummaries: " + to.Message);
                throw new Exception("Se exidio el tiempo de espera. No se pudo procesar la petición");
            }
            catch (Exception ex)
            {
                Log.Information("Error GetPoliciesSummaries: " + ex.Message);
                throw new Exception("Error Interno. No se pudo procesar la petición");
            }

        }

        private static string GetUserFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var sub = tokenS.Claims.First(claim => claim.Type == "sub").Value;

            string usercode = sub.Substring(sub.IndexOf("|") +1 );
            return usercode;
        }

       
    }
}
