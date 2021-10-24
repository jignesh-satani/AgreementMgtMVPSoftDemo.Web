using System;

namespace AgreementMgtMVPSoftDemo.API
{
     public static class TypeExtensions
     {
          public static DateTime ToDateTime(this object dateTimeObject)
          {
               if (dateTimeObject == null || dateTimeObject == DBNull.Value)
               {
                    return DateTime.MinValue;
               }
               return DateTime.ParseExact(dateTimeObject.ToString(), "MM/dd/yyyy", null);
          }

     }
}
