using Microsoft.AspNetCore.Mvc;

namespace AgreementMgtMVPSoftDemo.Web.Controllers
{
     public class AdminController : Controller
     {
          public IActionResult Index()
          {
               return View("dashboard");
          }
          public IActionResult Customer()
          {
               ViewData["page"] = "Customer";
               return View("customer");
          }
          public IActionResult Agreement(string email, string token)
          {
               ViewData["page"] = "Agreement";
               ViewData["email"] = email;
               ViewData["token"] = token;
               return View("Agreement");
          }

          public void Logout()
          {
              //clear session and JWT token
          }
     }
}
