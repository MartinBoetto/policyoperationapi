using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicyOperation.Models.Entidad;
using PolicyOperation.Models.ExternalEntities;

namespace PolicyOperation.ExternalServices.Interface
{
    public interface IPolicyCertificateDatail
    {
        Task<PolicyCertificateDatailDTO> GetPolicyDetails(PuidModel puid, string token);

        Task<PolicyCertificateDatailDTO> GetPolicyDetailsGW(PuidModel puid, string token, CeiboUserModel userModel);
    }
}
