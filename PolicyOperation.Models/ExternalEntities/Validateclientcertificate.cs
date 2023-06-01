using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyOperation.Models.ExternalEntities
{

    public class Rootobject
    {
        public Validateclientcertificate validateClientCertificate { get; set; }
        public Paging paging { get; set; }
        public Message[] messages { get; set; }
    }

    public class Validateclientcertificate
    {
        public bool success { get; set; }
    }

    public class Paging
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
    }

    public class Message
    {
        public string status { get; set; }
        public string code { get; set; }
        public string text { get; set; }
        public string help { get; set; }
    }

}
