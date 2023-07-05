using FluentValidation;
using Gss.CorporateApps.Infrastructure.Contracts.OperationResult;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PolicyOperation.Core.Entidad.Policy;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PolicyOperation.Core.Behavior
{

    //public class LogsBehavior

    public class LogsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
        where TResponse : IOperationResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="validators">Listado de validadores que están declarados
        /// en la instancia de <see cref="AbstractValidator{TRequest}"/></param>
        public LogsBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            // _validators = validators;
        }

        /// <summary>
        /// Controlar la ejecución previa a la ejecución del handler.
        /// </summary>
        /// <param name="request">Objeto que define parámetros que son necesarios para la ejecución del handler</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> que viaja a través de la llamada del handler  
        /// y que está a disposición para ser utilizado en la lógica del pipeline.</param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

                        
            // Log.Information("Request Api {@type} fallida: {@failures}", typeof(TRequest), failures.Select(f => f.ErrorMessage));
            var context = $"{_httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{_httpContextAccessor.HttpContext.Request.Host.Value}" +
                $"{_httpContextAccessor.HttpContext.Request.Path.Value}";

           
           Log.Information("URL Api: " + context.ToString());
           Log.Information("Request Api:" + JsonConvert.SerializeObject(request,Newtonsoft.Json.Formatting.None,
                                    new JsonSerializerSettings
                                    {
                                        NullValueHandling = NullValueHandling.Ignore
                                    })
                                    ); 
            return await next();
        }
    }


}
