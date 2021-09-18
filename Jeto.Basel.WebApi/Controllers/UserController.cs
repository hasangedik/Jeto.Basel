using System.Threading.Tasks;
using Jeto.Basel.Domain.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jeto.Basel.WebApi.Controllers
{
    public class UserController : ApiControllerBase
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>User information</returns>
        // [HttpGet("{id}")]
        // [ProducesResponseType(typeof(UserContract), 200)]
        // [ProducesResponseType(400)]
        // [ProducesResponseType(404)]
        // public async Task<ActionResult<UserContract>> GetCustomerAsync(Guid id)
        // {
        //     return Single(await QueryAsync(new GetCustomerQuery(id)));
        // }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="command">Info of user</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateCustomerAsync([FromBody] CreateUserCommand command)
        {
            return Ok(await CommandAsync(command));
        }
    }
}