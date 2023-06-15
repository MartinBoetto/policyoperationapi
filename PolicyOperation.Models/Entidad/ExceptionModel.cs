using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyOperation.Models.Entidad
{
    public class ExceptionModel
    {
        public List<MessageModel> messages { get; set; }
        public int httpStatusCode { get; set; } 
    }
    
}
