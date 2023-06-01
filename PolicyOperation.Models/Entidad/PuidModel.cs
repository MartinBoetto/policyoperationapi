using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyOperation.Models.Entidad
{
    public class PuidModel
    {
        public int bupID { get; set; }
        public int branchCode { get; set; }
        public string productCode { get; set; }
        public string certificateId { get; set;}
        public int certificateNumber { get; set; }
        public int coreId { get; set; }

        public PuidModel()
        {

        }
        public PuidModel(string puid )
        {
            var base64EncodedBytes = System.Convert.FromBase64String(puid);
            string[] words = System.Text.Encoding.UTF8.GetString(base64EncodedBytes).Split('-');
            this.bupID = Int32.Parse(words[0]);
            this.branchCode = Int32.Parse(words[1]);
            this.productCode = words[2];
            this.certificateId = words[3];
            this.certificateNumber = Int32.Parse(words[4]);
            this.coreId= Int32.Parse(words[5]); 
        }

    }


    
}
