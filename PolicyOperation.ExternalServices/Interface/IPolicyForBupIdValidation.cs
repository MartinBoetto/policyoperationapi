using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicyOperation.Models.ExternalEntities;

namespace PolicyOperation.ExternalServices.Interface
{
    public interface IPolicyForBupIdValidation
    {
        Task<Rootobject> ValidateclientcertificatesAsync(int bupId, int certificateId, string token);
    }
}
