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
    public class ProfileUserData : IProfileUserData
    {

        private string _token;
        private EndpointAddress endpointAddress;

        public ProfileUserData( EndpointAddress endpointAddress)
        {
            //this._token = token;
            this.endpointAddress = endpointAddress;
        }

             
        public async Task<ProfileUserDataDTO> GetData( string token)
        {
            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("CoreId", puid.coreId.ToString());
                client.DefaultRequestHeaders.Add("ApplicationId", "14");
                client.DefaultRequestHeaders.Add("CompanyCode", "1");
                client.DefaultRequestHeaders.Add("ClientTypeId", "1");

                string usercode = GetUserFromToken(token);
                string stringdatatoken = "{" + "userName" + ":" + "" + usercode + "" + "}";

                var tktAuthorization = Encoding.UTF8.GetBytes(stringdatatoken);
                var tktbase64String = Convert.ToBase64String(tktAuthorization, Base64FormattingOptions.InsertLineBreaks);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(tktbase64String);

                string stringdatatokenESB = "{" + "userName" + ":" + "" + usercode + ", bupId:0" + "}";
                var tktESBAuthorization = Encoding.UTF8.GetBytes(stringdatatokenESB);
                var tktESBbase64String = Convert.ToBase64String(tktESBAuthorization, Base64FormattingOptions.InsertLineBreaks);

                client.DefaultRequestHeaders.Add("AuthorizationESB", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiIxMSIsImV4cCI6MTY4NDQyODYzM30.5fHgqUDRJuNXQNLTMef4jFLWsx62mzb3aJYAs6Kg0mA");               
                


                string endpoint = "";
                       endpoint = $"{endpointAddress}?userCode={"21825"}" ;

                using (var Response = await client.GetAsync(endpoint))
                {
                    var result = await Response.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Models.ExternalEntities.ProfileUserDataDTO>(result);

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
