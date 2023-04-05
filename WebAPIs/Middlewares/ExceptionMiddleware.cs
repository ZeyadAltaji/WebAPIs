using Azure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using WebAPIs.Errors;

namespace WebAPIs.Middlewares
{
    public  class ExceptionMiddleware
    {
        //authorized
        private readonly RequestDelegate request;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;


        public ExceptionMiddleware(
            RequestDelegate request,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment environment


            )
        {
            this.request = request;
            this.logger = logger;
            this.environment = environment;

        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await request(context);
            }
            catch (Exception ex)
            {
                ErrorsAPIs response;
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
                if (environment.IsDevelopment())
                {
                    response = new ErrorsAPIs((int)statusCode, ex.Message, ex.StackTrace.ToString());
                }
                else
                {
                    response = new ErrorsAPIs((int)statusCode, ex.Message);
                }
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}
