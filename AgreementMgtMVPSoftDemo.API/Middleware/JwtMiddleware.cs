using AgreementMgtMVPSoftDemo.API.Auth;
using AgreementMgtMVPSoftDemo.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgreementMgtMVPSoftDemo.API.Middleware
{
     //   public static class JwtMiddleware
     //   {
     //  public static bool ValidateCurrentToken(string token)
     //  {			  
     //	   var tokenHandler = new JwtSecurityTokenHandler();
     //	   try
     //	   {
     //			tokenHandler.ValidateToken(token, new TokenValidationParameters
     //			{
     //				 ValidateIssuerSigningKey = true,
     //				 ValidateIssuer = true,
     //				 ValidateAudience = true						 
     //			}, out SecurityToken validatedToken);
     //	   }
     //	   catch
     //	   {
     //			return false;
     //	   }
     //	   return true;
     //  }
     //}
     public class JwtMiddleware
     {
          private readonly RequestDelegate _next;
          private readonly AppSettings _appSettings;

          public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
          {
               _next = next;
               _appSettings = appSettings.Value;
          }

          public async Task Invoke(HttpContext context, IUserRepository _IUserRepository)
          {
               var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
               var accessToken = context.Request.Headers[HeaderNames.Authorization];
               string authHeader = context.Request.Headers["Authorization"];

               if (token != null)
                    attachUserToContext(context, _IUserRepository, token);

               await _next(context);
          }

          private void attachUserToContext(HttpContext context, IUserRepository _IUserRepository, string token)
          {
               try
               {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                         ValidateIssuerSigningKey = false,
                         IssuerSigningKey = new SymmetricSecurityKey(key),
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                         ClockSkew = TimeSpan.FromMinutes(60)
                    }, out SecurityToken validatedToken); ;

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var email = jwtToken.Claims.First(x => x.Type == "Email").Value;

                    // attach user to context on successful jwt validation
                    context.Items["Email"] = _IUserRepository.Get(email);
               }
               catch(Exception ex)
               {
                    // do nothing if jwt validation fails
                    // user is not attached to context so request won't have access to secure routes
               }
          }
     }
}
