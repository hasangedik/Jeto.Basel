using System.Threading.Tasks;
using Jeto.Basel.Domain.Commands.Auth;
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> LoginAsync([FromBody] LoginCommand command)
        {
            return Ok(await CommandAsync(command));
        }
    }
}