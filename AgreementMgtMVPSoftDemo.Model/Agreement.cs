using System;

namespace AgreementMgtMVPSoftDemo.Model
{
     public class Agreement
     {
          public int Id { get; set; }
          public string UserId { get; set; }
          public int ProductGroupId { get; set; }
          public int ProductId { get; set; }
          public Nullable<System.DateTime> EffectiveDate { get; set; }
          public Nullable<System.DateTime> ExpirationDate { get; set; }
          public Nullable<decimal> NewPrice { get; set; }

          
     }
}
