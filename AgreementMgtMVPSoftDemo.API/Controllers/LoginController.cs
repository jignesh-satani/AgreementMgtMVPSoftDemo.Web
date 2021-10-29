using AgreementMgtMVPSoftDemo.API.Auth;
using AgreementMgtMVPSoftDemo.API.Model;
using AgreementMgtMVPSoftDemo.DAL;
using AgreementMgtMVPSoftDemo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
          [AllowAnonymous]
          [HttpPost("Authentication")]
          public IActionResult Authentication(AspNetUsers user)
          {
               AspNetUsers userCredential = _IUserRepository.Get(user.Email, user.PasswordHash);

               if (userCredential == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

               var response = _jwtAuth.GetToken(new User()
               {
                    Username = userCredential.Email,
                    FirstName = userCredential.FirstName,
                    LastName = userCredential.LastName,                    
               });
               if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

               return Ok(response);
          }
     }
}
