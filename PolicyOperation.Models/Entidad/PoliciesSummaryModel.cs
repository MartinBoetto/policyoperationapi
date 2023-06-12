using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyOperation.Models.Entidad
{
         public class PoliciesSummaryModel
            {
            public List<Policieslist> policiesList { get; set; }
            public List<MessageModel> messages { get; set; }
        }

        public class Policieslist
        {
            public string bupId { get; set; }
            public string puid { get; set; }
            public string referenceNumber { get; set; }
            public int certificateNumber { get; set; }
            public string branchCode { get; set; }
            public string branchName { get; set; }
            public object productCode { get; set; }
            public string productName { get; set; }
            public object policyTypeCode { get; set; }
            public string policyTypeName { get; set; }
            public bool isCurrent { get; set; }
            public int policyGroupTypeCode { get; set; }
            public string policyGroupTypeName { get; set; }
            public object stadisticalCode { get; set; }
            public int stadisticalGroup { get; set; }
            public string stadisticalGroupName { get; set; }
            public string startEffectiveDate { get; set; }
            public string endEffectiveDate { get; set; }
            public string statusCode { get; set; }
            public string statusName { get; set; }
            public string policyStatusCode { get; set; }
            public MetaModel meta { get; set; }
            public string displayStatus { get; set; }
            public bool isVigent { get; set; }
            public Vehicledata vehicleData { get; set; }
        }

        public class MetaModel
        {
            public string details { get; set; }
        }

        public class Vehicledata
        {
            public string modelBrandName { get; set; }
        }

        public class MessageModel
        {
            public string status { get; set; }
            public string code { get; set; }
            public string text { get; set; }
            public string help { get; set; }
        }

    }
