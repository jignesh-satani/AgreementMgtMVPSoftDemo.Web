using AgreementMgtMVPSoftDemo.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgreementMgtMVPSoftDemo.API.Auth
{
     public interface IJwtAuth
     {
          AuthenticateResponse GetToken(User user);
     }
}
