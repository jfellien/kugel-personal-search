using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Kugel.StaffSearch.Api;

static class ExceptionHandler
{
    public static void HandleException(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            ILogger? logger = app.ApplicationServices.GetService<ILogger>();
            IExceptionHandlerFeature? exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();

            if (exceptionDetails is null) return;

            string? exceptionMessage = exceptionDetails.Error?.Message;

            if(string.IsNullOrEmpty(exceptionMessage) == false && logger is not null)
            {
                string[]? routeValues = exceptionDetails.RouteValues?.Select(x => $"Name: {x.Key} Value:{x.Value}").ToArray();

                logger.LogError("Error on Endpoint {0} with Message {1}\r\nValues {2}", 
                    exceptionDetails.Endpoint, 
                    exceptionMessage,
                    routeValues is not null ? string.Join(';', routeValues) : "- No Values -");
            }

            context.Response.StatusCode = exceptionDetails.Error switch
            {
                HttpRequestException ex => (int)ex.StatusCode!,

                ArgumentException => (int)HttpStatusCode.BadRequest,
                
                SqlException 
                or DbUpdateException 
                or SystemException => (int)HttpStatusCode.InternalServerError,

                _ => (int)HttpStatusCode.InternalServerError,
            };

            await context.Response.CompleteAsync();
        });
    }
}
