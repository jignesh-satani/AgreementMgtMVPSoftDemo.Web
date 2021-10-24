using AgreementMgtMVPSoftDemo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public class UserRepository : IUserRepository
     {
          private readonly ApplicationContext context;
          //private DbSet<AspNetUsers> entities;
          string errorMessage = string.Empty;
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
