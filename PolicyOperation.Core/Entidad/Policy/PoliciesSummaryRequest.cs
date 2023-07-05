using Gss.CorporateApps.Core;
using System;

namespace PolicyOperation.Core.Entidad.Policy
{
        public class PoliciesSummaryRequest : IBaseRequest
        {
            public Policysearch policySearch { get; set; }
            public string token { get; set; }
            public string uid { get; set; } = Guid.NewGuid().ToString();
    }

        public class Policysearch
        {
            public Client client { get; set; }
            public string? branchCode { get; set; }
            public string? productCode { get; set; }
            public string? referenceNumber { get; set; }
            public string? certificateNumber { get; set; }
            public string? officialNumber { get; set; }
            public DateTime? startEffectiveDate { get; set; }
            public DateTime? endEffectiveDate { get; set; }
            public bool? isVigent { get; set; }
            public int? statisticsCode { get; set; }
            public string? personRoleCode { get; set; }
        }

        public class Client
        {
            public string bupId { get; set; }
            public string? firstName { get; set; }
            public string? lastName { get; set; }
            public string? documentType { get; set; }
            public string? documentNumber { get; set; }
        }



   
}
