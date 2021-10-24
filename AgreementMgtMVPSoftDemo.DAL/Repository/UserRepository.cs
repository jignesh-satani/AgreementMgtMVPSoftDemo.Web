using AgreementMgtMVPSoftDemo.Model;
using Microsoft.EntityFrameworkCore;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public class UserRepository : IUserRepository
     {
          private readonly ApplicationContext context;
          //private DbSet<AspNetUsers> entities;
          
          public UserRepository(ApplicationContext context)
          {
               this.context = context;
               //entities = context.Set<AspNetUsers>();
          }
          public AspNetUsers Get(string email, string password)
          {
               var obj = this.context.AspNetUsers.FirstOrDefaultAsync(u => u.Email.Equals(email) 
               && u.PasswordHash.Equals(password)).Result;
               return obj;
          }
          public AspNetUsers Get(string email)
          {
               var obj = this.context.AspNetUsers.FirstOrDefaultAsync(u => u.Email.Equals(email)).Result;
               return obj;
          }
     }
}
