using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public interface ILoggerManager
     {
          void LogError(string controller, string action, string message, string stackTrace, string user);
     }
}
