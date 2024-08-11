using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManagement.Users.Application.Common.Errors;
using TaskManagement.Users.Commons.Response;

namespace TaskManagement.Users.Api.Modules.GlobalException
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(ValidationExceptionCustom ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await JsonSerializer.SerializeAsync(context.Response.Body, 
                            new Response<Object> {Message = "Validation errors", Errors = ex.Errors});
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                _logger.LogError($"Exception details: {message}");

                var response = new Response<object>()
                {
                    Message = message,
                };

                await JsonSerializer.SerializeAsync(context.Response.Body, response);
            }
        }
    }
}