using System.Threading.Tasks;
using Jeto.Basel.Domain.Messages.Requests.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jeto.Basel.WebApi.Controllers
{
    public class AuthController : ApiControllerBase
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }
        
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> LoginAsync([FromBody] LoginCommandRequest command)
        {
            return Ok(await CommandAsync(command));
        }
    }
}