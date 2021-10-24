using AgreementMgtMVPSoftDemo.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public interface IAgreementRepository
     {
          IEnumerable<UserAgreements> GetUserAgreements(string email);
     }
}
