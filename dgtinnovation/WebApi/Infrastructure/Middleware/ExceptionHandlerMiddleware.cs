using Application.ErrorHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Infrastructure.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandlerExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandlerExceptionAsync(HttpContext context, Exception ex,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            object errors = null;
            switch (ex)
            {
                case ExceptionHandler me:
                    logger.LogError(ex, "Handler Error");
                    errors = me.Error;
                    context.Response.StatusCode = (int)me.Code;
                    break;
                case Exception e:
                    logger.LogError(ex, "Server Error");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(new { errores = errors });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
