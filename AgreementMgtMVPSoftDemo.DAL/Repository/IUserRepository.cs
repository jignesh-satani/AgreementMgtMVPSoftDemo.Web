using AgreementMgtMVPSoftDemo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public interface IUserRepository
     {
          public AspNetUsers Get(string email, string password);
          public AspNetUsers Get(string email);
     }
}
