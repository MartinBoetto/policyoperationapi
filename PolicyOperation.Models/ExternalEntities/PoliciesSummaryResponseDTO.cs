using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyOperation.Models.ExternalEntities
{

        public class PoliciesSummaryResponseDTO
    {
            public List<Policy> policies { get; set; }
            public Paging paging { get; set; }
            public List<MessageDTO> messages { get; set; }
        }

        public class Policy
        {
            public int coreId { get; set; }
            public string bupId { get; set; }
            public string displayStatus { get; set; }
            public string referenceNumber { get; set; }
            public int certificateNumber { get; set; }
            public int certificateId { get; set; }
            public string branchCode { get; set; }
            public string branchName { get; set; }
            public string productCode { get; set; }
            public string productName { get; set; }
            public string policyTypeCode { get; set; }
            public string policyTypeName { get; set; }
            public bool isVigent { get; set; }
            public int policyGroupTypeCode { get; set; }
            public string policyGroupTypeName { get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public string statusCode { get; set; }
            public string statusName { get; set; }
            public string policyStatusCode { get; set; }
            public Vehicledata vehicleData { get; set; }
        }

        public class Vehicledata
        {
            public string modelBrandName { get; set; }
            public int assistanceTypeCode { get; set; }
            public string assistanceTypeDescription { get; set; }
        }

        public class MessageDTO
        {
            public string status { get; set; }
            public string code { get; set; }
            public string text { get; set; }
            public string help { get; set; }
        }

    }

