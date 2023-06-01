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

namespace PolicyOperation.ExternalServices.Service
{
    public class PolicyForBupIdValidation : IPolicyForBupIdValidation
    {

        private string _token;
        private EndpointAddress endpointAddress;

        public PolicyForBupIdValidation( EndpointAddress endpointAddress)
        {
            //this._token = token;
            this.endpointAddress = endpointAddress;
        }
        public async Task<Rootobject> ValidateclientcertificatesAsync(int bupId, int certificateId, string token)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("CoreId", "2");
                client.DefaultRequestHeaders.Add("ApplicationId", "14");
                client.DefaultRequestHeaders.Add("CompanyCode", "1");
                client.DefaultRequestHeaders.Add("ClientTypeId", "1");
                
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token);



                string endpoint = $"{endpointAddress}?bupId={bupId}&certificateId={certificateId}";

                using (var Response = await client.GetAsync(endpoint))
                {
                    var result = await Response.Content.ReadAsStringAsync();
                    var responseBup = JsonConvert.DeserializeObject<Rootobject>(result);
                    

                    
                    return responseBup;
                }
            }
                        
        }
    }
}
