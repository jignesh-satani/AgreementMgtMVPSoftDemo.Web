using AgreementMgtMVPSoftDemo.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public class AgreementRepository : IAgreementRepository
     {
          private readonly ApplicationContext _context;
          public AgreementRepository(ApplicationContext context)
          {
               _context = context;
          }
          public IEnumerable<UserAgreements> GetUserAgreements(string email)
          {
               return _context.UserAgreements.Where(t => t.Email.Trim().ToLower() == email.Trim().ToLower()).AsEnumerable();        
          }
     }
}
