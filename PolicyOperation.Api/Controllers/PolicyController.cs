using Microsoft.AspNetCore.Mvc;
using Gss.CorporateApps.Common.Controllers;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using PolicyOperation.Models.Entidad;
using PolicyOperation.Core.Entidad.Policy;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;

namespace PolicyOperation.Api.Controllers
{
    [ApiController]
       
    public class PolicyController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(PolicyModel), 401)]
        [SwaggerOperation("Obtener una polizas por identificador PUID")]
        [Route("/policies/{puid?}")]
        //[Authorize]        
        public async Task<IActionResult> GetPolicyById(string? puid, [FromHeader] string? Authorization)
        {
            //HttpContext.Response.Headers.Add("x-my-custom-header", "individual response");
            PolicyRequest requestModel = new PolicyRequest
            {
                puid = puid,
                token = Authorization
            };
            /*OkObjectResult result = (OkObjectResult)await CallHandlerFromRequestAsync(requestModel);
            return Ok(result.Value);
            */
            var result = await CallHandlerFromRequestAsync(requestModel) as OkObjectResult;


            //var obj = result as ObjectResult;
            if (result == null)
            {
                return NoContent();
            }else if(result.Value != null)
            {
                var valurResult = result.Value as PolicyModel;
                if (valurResult.policyDetails != null)
                    return Ok(valurResult.policyDetails);
                else
                    return StatusCode(500, valurResult.messages);
;            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(PolicyModel), 401)]
        [SwaggerOperation("Obtener todas las Polizas.")]
        [Route("/policies")]
        public async Task<IActionResult> GetAllPolicy()
        {
            return await CallHandlerFromRequestAsync(null);
        }
    }


}
