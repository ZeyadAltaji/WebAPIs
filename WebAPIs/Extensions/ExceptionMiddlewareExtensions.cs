﻿using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace WebAPIs.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler(
                    option => option.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            var exception = context.Features.Get<IExceptionHandlerFeature>();
                            if (exception != null)
                            {
                                await context.Response.WriteAsync(exception.Error.Message);
                            }
                        }));
            }
        }

    }
}
