using log4net.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CarShowroom.UI.Filters
{
    public class ExceptionHandlingFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ExceptionHandlingFilterAttribute>>();

            logger.LogWarning(context.Exception, "On path: {Path} got exception: {Message}", context.HttpContext.Request.Path, context.Exception.Message);

            if (context.Exception is DataException)
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Content = "Database is unavailable. Try again later.",
                    ContentType = "text/plain"
                };
            }
            else if (context.Exception is DbUpdateException)
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Content = "Error while saving to the database.",
                    ContentType = "text/plain"
                };
            }
        }
    }
}
