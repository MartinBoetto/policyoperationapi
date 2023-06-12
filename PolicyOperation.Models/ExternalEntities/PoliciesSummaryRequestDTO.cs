using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyOperation.Models.ExternalEntities
{
   

        public class PoliciesSummaryRequestDTO
        {
            public PolicyDTO policy { get; set; }
        }

        public class PolicyDTO
    {
            public Client? client { get; set; }
            public string? branchCode { get; set; }
            public string? productCode { get; set; }
            public string? referenceNumber { get; set; }
            public int? certificateNumber { get; set; }
            public int? certificateId { get; set; }
            public string? policyProposalNumber { get; set; }
            public string producerCode { get; set; }

            public string? officialNumber { get; set; }
            public DateTime? startDate { get; set; }
            public DateTime? endDate { get; set; }
            public int? statisticsCode { get; set; }
            public string? personRoleCode { get; set; }
        }

        public class Client
        {
            public string bupId { get; set; }
            public string? firstName { get; set; }
            public string? lastName { get; set; }
            public string? registeredName { get; set; }
            public string? identificationNumber { get; set; }
        }

    }

