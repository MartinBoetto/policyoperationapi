using PolicyOperation.Models.ExternalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyOperation.Models.Entidad
{
        public class PolicyModel
        {
            public Policydetails policyDetails { get; set; }
            public List<Message> messages { get; set; }
        }

        public class Policydetails
        {
            public int referenceNumber { get; set; }
            public int certificateNumber { get; set; }
            public int branchCode { get; set; }
            public string branchName { get; set; }
            public int productCode { get; set; }
            public string productName { get; set; }
            public string officialNumber { get; set; }
            public string startEffectiveDate { get; set; }
            public string endEffectiveDate { get; set; }
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
            public string insuredRisks { get; set; }
            public string additionalGoods { get; set; }
            public string additionalCoverages { get; set; }
            public string coverages { get; set; }
        }

   
}