using AgreementMgtMVPSoftDemo.API.Auth;
using AgreementMgtMVPSoftDemo.DAL;
using AgreementMgtMVPSoftDemo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgreementMgtMVPSoftDemo.API.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class LoginController : ControllerBase
     {
          private readonly IJwtAuth _jwtAuth;
          private readonly IUserRepository _IUserRepository;
          public LoginController(IJwtAuth jwtAuth, IUserRepository IUserRepository)
          {
               this._jwtAuth = jwtAuth;
               _IUserRepository = IUserRepository;
          }

          //[HttpPost("Authentication/{username}/{password}")]
          [HttpPost("Authentication")]
          public IActionResult Authentication([FromBody] AspNetUsers user)
          {
               AspNetUsers userCredential = _IUserRepository.Get(user.Email, user.PasswordHash);

               if (userCredential == null)
                    return new JsonResult(new TokenResponse() { Success = false, Message = "InvalidToken" });

               var token = _jwtAuth.GetToken(userCredential.UserName);
               if (token == null)
                    return new JsonResult(new TokenResponse() { Success = false, Message = "InvalidToken" });

               return new JsonResult(new TokenResponse() { Success = true, Message = "Success"
                    , UserEmail= userCredential.Email
                    , Token = token });
          }
     }
}
