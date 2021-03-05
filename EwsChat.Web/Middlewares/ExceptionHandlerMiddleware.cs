using EwsChat.Data.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EwsChat.Web.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
                throw;
            }
        }

        private async Task HandleException(HttpContext context, Exception error)
        {
            context.Response.ContentType = "application/json";

            switch (error)
            {
                case UserNotFoundException _:
                case RoomNotFoundException _:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case InvalidMessageException _:
                case UserAlreadyExistsException _:
                case NullReferenceException _:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

            }
            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await context.Response.WriteAsync(result);

        }
    }
}
