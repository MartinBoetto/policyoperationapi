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
            try { 
                using (HttpClient client = new HttpClient())
                {
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
                
                        using (var Response = await client.PostAsync(endpoint, stringContent))
                        {
                            var result = await Response.Content.ReadAsStringAsync();
                            var response = JsonConvert.DeserializeObject<Models.ExternalEntities.PoliciesSummaryResponseDTO>(result);

                            return response;
                          }
               
                    }
                }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
