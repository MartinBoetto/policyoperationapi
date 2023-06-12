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
using System.Net;

namespace PolicyOperation.ExternalServices.Service
{
    public class PolicyCertificateDatail : IPolicyCertificateDatail
    {

        private string _token;
        private EndpointAddress endpointAddress;

        public PolicyCertificateDatail( EndpointAddress endpointAddress)
        {
            //this._token = token;
            this.endpointAddress = endpointAddress;
        }

        private string DecodePuid(string puid)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(puid);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

        }

      
        public async Task<PolicyCertificateDatailDTO> GetPolicyDetails(PuidModel puid, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("CoreId", puid.coreId.ToString());
                client.DefaultRequestHeaders.Add("ApplicationId", "14");
                client.DefaultRequestHeaders.Add("CompanyCode", "1");
                client.DefaultRequestHeaders.Add("ClientTypeId", "1");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token);



                string endpoint = "";
                if (puid.coreId == 1)
                    endpoint = $"{endpointAddress}?certificateId={puid.certificateId}" +
                        $"&origenCode={13}" +
                        $"&aplicationCode={17}";

                if (puid.coreId == 2)
                    endpoint = $"{endpointAddress}?referenceNumber={puid.certificateId}" +
                        $"certificateNumber={puid.certificateNumber}";

                using (var Response = await client.GetAsync(endpoint))
                {
                    var result = await Response.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<Models.ExternalEntities.PolicyCertificateDatailDTO>(result);

                    return response;
                }
            }
        }



        public async Task<PolicyCertificateDatailDTO> GetPolicyDetailsGW(PuidModel puid, string token, CeiboUserModel userModel)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("CoreId", puid.coreId.ToString());
                    client.DefaultRequestHeaders.Add("ApplicationId", "14");
                    client.DefaultRequestHeaders.Add("CompanyCode", "1");
                    client.DefaultRequestHeaders.Add("ClientTypeId", "1");


                    ESBTokenModel tokenModel = new ESBTokenModel();
                    tokenModel.userName = userModel.UserCode.ToString();
                    tokenModel.bupId =  userModel.BupID;
                    string output = JsonConvert.SerializeObject(tokenModel);

                    var tktESBAuthorization = Encoding.UTF8.GetBytes(output);
                    string tktESBbase64String = Convert.ToBase64String(tktESBAuthorization).TrimEnd('=');//, Base64FormattingOptions.InsertLineBreaks);


                    //client.DefaultRequestHeaders.Add("Authorization", tktESBbase64String);
                    client.DefaultRequestHeaders.Add("Authorization", "ewogICJ1c2VyTmFtZSI6ICIzMjQ0NCIsCiAgImJ1cElkIjogNTI2MTMyNwp9");

                    string endpoint = $"{endpointAddress}?referenceNumber={puid.certificateId}" +
                            $"&certificateNumber={puid.certificateNumber}";

                    using (var Response = await client.GetAsync(endpoint))
                    {
                        var result = await Response.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<Models.ExternalEntities.PolicyCertificateDatailDTO>(result);

                        return response;
                    }
                } catch (Exception ex) 
                { 
                    Console.WriteLine(ex); 
                    return null;
                }
            }
        }



    }
}
