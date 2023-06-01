using System;
using System.ServiceModel;

namespace PolicyOperation.ExternalServices
{
    public class ComplexHttpBinding : BaseHttpBinding
    {
        public ComplexHttpBinding() : base()
        {
            TextEncoding = System.Text.Encoding.UTF8;
            ReaderQuotas = new System.Xml.XmlDictionaryReaderQuotas()
            {
                MaxDepth = 32,
                MaxStringContentLength = 2147483647,
                MaxArrayLength = 2147483647,
                MaxBytesPerRead = 2147483647,
                MaxNameTableCharCount = 2147483647
            };
            ReceiveTimeout = new TimeSpan(0, 10, 0);
            BypassProxyOnLocal = false;
            UseDefaultWebProxy = true;
            AllowCookies = false;
            TransferMode = TransferMode.Buffered;
            MaxBufferPoolSize = 524288;
            MaxBufferSize = 26214400;
            MaxReceivedMessageSize = 26214400;
            Security = new BasicHttpSecurity()
            {
                Mode = BasicHttpSecurityMode.None,
                Transport = new HttpTransportSecurity
                {
                    ClientCredentialType = HttpClientCredentialType.None,
                    ProxyCredentialType = HttpProxyCredentialType.None,
                },
                Message = new BasicHttpMessageSecurity
                {
                    ClientCredentialType = BasicHttpMessageCredentialType.UserName
                }
            };
        }
    }
}
