using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicyOperation.Models.Entidad;
using PolicyOperation.Models.ExternalEntities;

namespace PolicyOperation.ExternalServices.Interface
{
    public interface IPoliciesSummary
    {
        Task<PoliciesSummaryResponseDTO> GetPoliciesSummaries(PoliciesSummaryRequestDTO policySummaryrequestDTO, CeiboUserModel userModel);
    }
}
