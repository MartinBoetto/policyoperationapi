namespace PolicyOperation.Core.Entidad.CaseDeUso
{
    using Gss.CorporateApps.Core;
    using Gss.CorporateApps.Infrastructure.Contracts.OperationResult;
    using System.Threading;
    using System.Threading.Tasks;

    public class CasoDeUsoHandler : BaseHandler<CasoDeUsoRequest>
    {
        protected override async Task<IOperationResult> ExecuteAsync(CasoDeUsoRequest request, CancellationToken cancellationToken)
        {
            // ... 
            return new SuccessResult();
        }
    }
}
