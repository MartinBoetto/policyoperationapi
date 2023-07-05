using Gss.CorporateApps.Core;
using System;

namespace PolicyOperation.Core.Entidad.Policy
{
    public class PolicyRequest : IBaseRequest
    {
        public string? puid { get; set; }
        public string token { get; set; }
        public string uid { get; set; } = Guid.NewGuid().ToString();
    }
}
