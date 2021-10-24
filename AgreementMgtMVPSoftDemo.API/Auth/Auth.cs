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
          private readonly string key;
          public JwtAuth(string key)
          {
               this.key = key;
          }
          public string GetToken(string username)
          {

               // 1. Create Security Token Handler
               var tokenHandler = new JwtSecurityTokenHandler();

               // 2. Create Private Key to Encrypted
               var tokenKey = Encoding.ASCII.GetBytes(key);

               //3. Create JETdescriptor
               var tokenDescriptor = new SecurityTokenDescriptor()
               {
                    Subject = new ClaimsIdentity(
                       new Claim[]
                       {
                        new Claim(ClaimTypes.Name, username)
                       }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                       new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
               };
               //4. Create Token
               var token = tokenHandler.CreateToken(tokenDescriptor);

               // 5. Return Token from method
               return tokenHandler.WriteToken(token);
          }
     }
}
