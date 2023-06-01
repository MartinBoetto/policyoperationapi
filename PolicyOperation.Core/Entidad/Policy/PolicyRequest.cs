using Gss.CorporateApps.Core;

namespace PolicyOperation.Core.Entidad.Policy
{
    public class PolicyRequest : IBaseRequest
    {
        public string? puid { get; set; }
        public string token { get; set; }
    }
}
