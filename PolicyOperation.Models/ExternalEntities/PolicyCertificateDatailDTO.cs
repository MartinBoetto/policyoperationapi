using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyOperation.Models.ExternalEntities
{

        public class PolicyCertificateDatailDTO
       {
            public Certificatedetail certificateDetail { get; set; }
            public Paging paging { get; set; }
            public Message[] messages { get; set; }
        }

        public class Certificatedetail
        {
            public int coreId { get; set; }
            public string policyStatusName { get; set; }
            public string branchName { get; set; }
            public int branchCode { get; set; }
            public string offeringName { get; set; }
            public string requestNumberGW { get; set; }
            public string officialNumber { get; set; }
            public string policyPeriodStartEffectiveDate { get; set; }
            public string policyPeriodEndEffectiveDate { get; set; }
            public string policyTypeCode { get; set; }
            public string policyTypeName { get; set; }
            public string conditionCode { get; set; }
            public string stateName { get; set; }
            public float coveredItemPremiun { get; set; }
            public float policyPeriodPremiun { get; set; }
            public float chargePercentageQuantity { get; set; }
            public float discountPercentage { get; set; }
            public string email { get; set; }
            public Meta meta { get; set; }
        }

        public class Meta
        {
            public string client { get; set; }
            public string intermediaries { get; set; }
            public string payment { get; set; }
            public string risk { get; set; }
            public string additionalGoods { get; set; }
            public string additionalData { get; set; }
            public string coverages { get; set; }
        }
    
}
