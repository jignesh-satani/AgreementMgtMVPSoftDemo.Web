using AgreementMgtMVPSoftDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgreementMgtMVPSoftDemo.API
{
     public class DataTableResponse<T> where T:class
     {
          public string draw { get; set; }
          public int recordsTotal { get; set; }
          public int recordsFiltered { get; set; }   
          public AspNetUsers aspNetUser { get; set; }
          public IEnumerable<T> data { get; set; }
     }
}
