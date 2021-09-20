using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Jeto.Basel.Common.Constants;
using Jeto.Basel.Common.Resources;
using Jeto.Basel.Domain.Messages.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Jeto.Basel.WebApi.Middlewares
{
    /// <summary>
    /// Global Exception handler middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        /// <summary>
        /// Global Exception handler middleware constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="next">Next middleware</param>
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        /// <summary>
        /// Middleware invoke method
        /// </summary>
        /// <param name="httpContext">Http Context</param>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var response = new BaseResponse
            {
                HasError = true
            };
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException exception)
            {
                var errorMessages = !exception.Errors.Any() ? new List<string> { exception.Message } : exception.Errors.Select(p => p.ErrorMessage).ToList();
                
                var validationResponse = response with
                {
                    ErrorCode = ((int)HttpStatusCode.BadRequest).ToString(),
                    ErrorMessages = errorMessages,
                    Message = Literals.ValidationError
                };

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                httpContext.Response.ContentType = AppConstants.JsonContentType;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(validationResponse));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "{Message}", exception.Message);

                var exceptionResponse = response with
                {
                    ErrorCode = ((int)HttpStatusCode.InternalServerError).ToString(),
                    ErrorMessages = new List<string> { Literals.ServerError },
                    Message = Literals.ServerError
                };

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = AppConstants.JsonContentType;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(exceptionResponse));
            }
        }
    }
}