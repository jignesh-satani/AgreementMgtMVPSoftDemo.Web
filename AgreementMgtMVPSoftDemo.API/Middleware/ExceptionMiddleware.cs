using AgreementMgtMVPSoftDemo.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AgreementMgtMVPSoftDemo.API.Middleware
{
     public class ExceptionMiddleware
     {
          private readonly RequestDelegate _next;
          private readonly ILoggerManager _logger;
          public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
          {
               _logger = logger;
               _next = next;
          }
          public async Task InvokeAsync(HttpContext httpContext)
          {
               try
               {
                    //string controllerName = httpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>().ControllerName;
                    //string actionName = httpContext.GetEndpoint().Metadata.GetMetadata<ControllerActionDescriptor>().ActionName;
                    await _next(httpContext);
               }
               catch (AccessViolationException avEx)
               {
                    //_logger.LogError($"A new violation exception has been thrown: {avEx}");
                    await HandleExceptionAsync(httpContext, avEx);
               }
               catch (Exception ex)
               {
               //log error
                    await HandleExceptionAsync(httpContext, ex);
               }
          }

          private async Task HandleExceptionAsync(HttpContext context, Exception exception)
          {
               context.Response.ContentType = "application/json";
               context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

               var message = "Internal Server Error from the custom middleware";

               await context.Response.WriteAsync(new ErrorDetails()
               {
                    StatusCode = context.Response.StatusCode,
                    Message = message
               }.ToString());
          }
     }
}
