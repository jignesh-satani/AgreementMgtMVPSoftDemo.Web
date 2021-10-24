namespace AgreementMgtMVPSoftDemo.DAL
{
     public interface ILoggerManager
     {
          void LogError(string controller, string action, string message, string stackTrace, string user);
     }
}
