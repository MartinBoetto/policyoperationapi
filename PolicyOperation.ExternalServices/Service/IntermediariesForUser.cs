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
using Serilog;

namespace PolicyOperation.ExternalServices.Service
{
    public class IntermediariesForUser : IIntermediariesForUser
    {

        private string _token;
        private EndpointAddress endpointAddress;

        public IntermediariesForUser( EndpointAddress endpointAddress)
        {
            //this._token = token;
            this.endpointAddress = endpointAddress;
        }

             
        public async Task<IntermediariesUserDTO> GetIntermediariesForUser(CeiboUserModel ceiboUserModel, string token)
        {
            try
            {
                Log.Information("Metodo GetIntermediariesForUser");
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15);
                    //client.DefaultRequestHeaders.Add("CoreId", puid.coreId.ToString());
                    client.DefaultRequestHeaders.Add("ApplicationId", "14");
                    client.DefaultRequestHeaders.Add("CompanyCode", "1");
                    client.DefaultRequestHeaders.Add("ClientTypeId", "1");

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


                    string endpoint = "";
                    endpoint = $"{endpointAddress}?userCode={ceiboUserModel.UserCode}";

                    Log.Information("Invocaion BackEnd: " + endpoint);
                    Log.Information("Request : " + ceiboUserModel.UserCode);
                    Log.Information("Token : " + token);

                    using (var Response = await client.GetAsync(endpoint))
                    {
                        var result = await Response.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<Models.ExternalEntities.IntermediariesUserDTO>(result);
                        Log.Information("Response :" + result);
                        return response;
                    }
                }
            }
            catch (TaskCanceledException to)
            {
                Log.Information("Error GetIntermediariesForUser: " + to.Message);
                throw new Exception("Se exidio el tiempo de espera. No se pudo procesar la petición");
            }
            catch (Exception ex)
            {
                Log.Information("Error GetIntermediariesForUser: " + ex.Message);
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
