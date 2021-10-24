using AgreementMgtMVPSoftDemo.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public class AgreementRepository : IAgreementRepository
     {
          private readonly ApplicationContext _context;
          private DbSet<UserAgreements> entities;
          public AgreementRepository(ApplicationContext context)
          {
               _context = context;
               entities = context.Set<UserAgreements>();
          }
          public IEnumerable<UserAgreements> GetUserAgreements(string email)
          {
               return _context.UserAgreements.Where(t => t.Email.Trim().ToLower() == email.Trim().ToLower()).AsEnumerable();        
          }
     }
}
