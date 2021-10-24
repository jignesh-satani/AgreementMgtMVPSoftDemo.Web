using System;
using System.Collections.Generic;

namespace AgreementMgtMVPSoftDemo.Model
{
     public class AspNetUsers
     {
          public AspNetUsers()
          {
               //this.Agreement = new HashSet<Agreement>();
          }

          public string Id { get; set; }
          public string Email { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public bool EmailConfirmed { get; set; }
          public string PasswordHash { get; set; }
          public string PhoneNumber { get; set; }
          public string UserName { get; set; }          
          //public virtual ICollection<Agreement> Agreement { get; set; }
     }
}
