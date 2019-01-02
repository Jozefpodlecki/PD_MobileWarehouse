using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebApiServer.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        internal class ExceptionDto
        {
            public string Message { get; set; }
            public string Source { get; set; }
            public string StackTrace { get; set; }
            public string HelpLink { get; set; }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var exceptionDto = new ExceptionDto
            {
                Message = exception.Message,
                Source = exception.Source,
                StackTrace = exception.StackTrace
            };

            var result = JsonConvert.SerializeObject(exceptionDto);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
