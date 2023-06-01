namespace PolicyOperation.Infrastructure.AppConfiguration
{
    public sealed class AppConfig
    {
        //public class SeccionAppSettings
        //{
        //    public static string Clave { get; set; }
        //}


        public class ConnectionStrings
        {
          public static string TimePro { get; set; }

        }

        public class Logging
        {
            public static string PathUrl { get; set; }
        }

        public class Validateclientcertificates
        {
            public static string BaseUrl { get; set; }
        }

        public class PolicyCertificateDatail
        {
            public static string BaseUrl { get; set; }
        }

        public class ProfileUserData
        {
            public static string BaseUrl { get; set; }
        }

    }
}

