using System;
using System.ServiceModel;

namespace PolicyOperation.ExternalServices
{
    public class BaseHttpBinding : BasicHttpBinding
    {
        public BaseHttpBinding()
        {
            OpenTimeout = new TimeSpan(0, 2, 0);
            CloseTimeout = new TimeSpan(0, 2, 0);
            SendTimeout = new TimeSpan(0, 2, 0);
            ReceiveTimeout = new TimeSpan(0, 2, 0);
            MaxReceivedMessageSize = 2147483647;
            MaxBufferPoolSize = 2147483647;
            MaxBufferSize = 2147483647;
        }
    }
}
