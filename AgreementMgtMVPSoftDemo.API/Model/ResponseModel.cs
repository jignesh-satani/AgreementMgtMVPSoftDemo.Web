﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgreementMgtMVPSoftDemo.API
{
     public class TokenResponse
     {
          public bool Success { get; set; }
          public string UserEmail { get; set; }
          public string Message { get; set; }
          public string Token { get; set; }
     }
}
