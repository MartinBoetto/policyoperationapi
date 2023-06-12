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
            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("CoreId", puid.coreId.ToString());
                client.DefaultRequestHeaders.Add("ApplicationId", "14");
                client.DefaultRequestHeaders.Add("CompanyCode", "1");
                client.DefaultRequestHeaders.Add("ClientTypeId", "1");

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


                string endpoint = "";
                       endpoint = $"{endpointAddress}?userCode={ceiboUserModel.UserCode}" ;

                using (var Response = await client.GetAsync(endpoint))
                {
                    var result = await Response.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Models.ExternalEntities.IntermediariesUserDTO>(result);

                    return response;
                }
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
