using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyOperation.Models.ExternalEntities
{

    public class ProfileUserDataDTO
    {
        public Profileuserdata profileUserData { get; set; }
        public Message[] messages { get; set; }
    }

    public class Profileuserdata
    {
        public int userCode { get; set; }
        public int bupId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public int personType { get; set; }
        public bool employee { get; set; }
        public bool isDealer { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public int zoneCode { get; set; }
        public string zoneName { get; set; }
        public int userStatusCode { get; set; }
        public string userStatusName { get; set; }
        public int companyCode { get; set; }
        public int userTypeCode { get; set; }
        public Office[] offices { get; set; }
        public Profile[] profiles { get; set; }
        public Document document { get; set; }
        public Tributarycode tributaryCode { get; set; }
    }

    public class Document
    {
        public string identificationNumber { get; set; }
        public string identificationTypeCode { get; set; }
        public string identificationTypeName { get; set; }
    }

    public class Tributarycode
    {
        public string taxIdentificationNumber { get; set; }
        public string taxIdentificationTypeCode { get; set; }
        public string taxIdentificationTypeName { get; set; }
    }

    public class Office
    {
        public int idOffice { get; set; }
        public string nameOffice { get; set; }
        public string visualName { get; set; }
    }

    public class Profile
    {
        public int profileCode { get; set; }
        public string profileDescription { get; set; }
    }

   

}
