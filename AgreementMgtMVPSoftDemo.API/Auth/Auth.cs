using AgreementMgtMVPSoftDemo.API.Middleware;
using AgreementMgtMVPSoftDemo.API.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgreementMgtMVPSoftDemo.API.Auth
{
     public class JwtAuth : IJwtAuth
     {
          private readonly AppSettings _appSettings;
          public JwtAuth(IOptions<AppSettings> appSettings)
          {
               _appSettings = appSettings.Value;
          }
          public AuthenticateResponse GetToken(User user)
          {


               // 1. Create Security Token Handler
               var tokenHandler = new JwtSecurityTokenHandler();

               // 2. Create Private Key to Encrypted
               var tokenKey = Encoding.ASCII.GetBytes(_appSettings.Secret);

               //3. Create JETdescriptor              
               var tokenDescriptor = new SecurityTokenDescriptor()
               {
                    Issuer = "http://localhost:4898/",
                    Audience = "http://localhost:56802/",
                    Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim("Email", user.Username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                       new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
               };
               //4. Create Token
               var token = tokenHandler.CreateToken(tokenDescriptor);

               // 5. Return Token from method
               //return tokenHandler.WriteToken(token);
               return new AuthenticateResponse(user, tokenHandler.WriteToken(token));
          }
     }
}
