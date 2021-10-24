using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementMgtMVPSoftDemo.Model
{
     public class UserAgreements
     {
          public int Id { get; set; }
          public string UserId { get; set; }
          public int ProductGroupId { get; set; }
          public int ProductId { get; set; }
          public string EffectiveDate { get; set; }
          public string ExpirationDate { get; set; }
          public Nullable<decimal> NewPrice { get; set; }
          public string ProductNumber { get; set; }
          public string ProductDescription { get; set; }
          public Nullable<decimal> ProductPrice { get; set; }
          public string GroupDescription { get; set; }
          public string GroupCode { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Email { get; set; }
     }
}
