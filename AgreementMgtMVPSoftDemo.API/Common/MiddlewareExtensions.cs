using AgreementMgtMVPSoftDemo.API.Middleware;
using AgreementMgtMVPSoftDemo.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace AgreementMgtMVPSoftDemo.API
{
     public static class MiddlewareExtensions
     {
          public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
          {
               app.UseExceptionHandler(appError =>
               {
                    appError.Run(async context =>
                    {
                         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                         context.Response.ContentType = "application/json";

                         var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                         if (contextFeature != null)
                         {
                              //logger.LogError($"Something went wrong: {contextFeature.Error}");

                              await context.Response.WriteAsync(new ErrorDetails()
                              {
                                   StatusCode = context.Response.StatusCode,
                                   Message = "Internal Server Error."
                              }.ToString());
                         }
                    });
               });
          }

          public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
          {
               app.UseMiddleware<ExceptionMiddleware>();
          }

          public static void SetObject<T>(this ISession session, string key, T value)
          {
               session.SetString(key, JsonConvert.SerializeObject(value));
          }

          public static T GetObject<T>(this ISession session, string key)
          {
               var value = session.GetString(key);
               return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
          }
     }
}
