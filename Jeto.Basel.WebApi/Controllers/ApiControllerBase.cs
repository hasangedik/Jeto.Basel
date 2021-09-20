using System;
using System.Threading.Tasks;
using Jeto.Basel.Domain.Messages.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jeto.Basel.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ApiControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException();
        }
        
        protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
        {
            return await _mediator.Send(query);
        }

        protected ActionResult<T> Single<T>(T data)
        {
            if (data == null) return NotFound();
            return Ok(data);
        }
        
        protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
        {
            return await _mediator.Send(command);
        }
        
        [NonAction]
        protected new OkObjectResult Ok() => Ok(null);

        [NonAction]
        public override OkObjectResult Ok(object value)
        {
            var response = new BaseResponse
            {
                Message = "OK",
                Result = value,
            };

            return base.Ok(response);
        }
    }
}