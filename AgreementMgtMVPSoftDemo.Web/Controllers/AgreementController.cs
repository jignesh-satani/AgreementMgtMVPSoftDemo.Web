﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgreementMgtMVPSoftDemo.Web.Controllers
{
     public class AgreementController : Controller
     {
          public IActionResult Index()
          {
               return View("Agreement");
          }
     }
}
