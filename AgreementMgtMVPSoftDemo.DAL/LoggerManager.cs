using System;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public class LoggerManager : ILoggerManager
     {
          public void LogError(string controller, string action, string message, string stackTrace, string user)
          {
               throw new NotImplementedException();
          }
     }
}
