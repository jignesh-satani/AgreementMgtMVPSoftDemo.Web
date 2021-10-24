using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public class GenericRepository<T>: IGenericRepository<T> where T:class
     {
          private readonly ApplicationContext context;
          private DbSet<T> entities;
          string errorMessage = string.Empty;
          public GenericRepository(ApplicationContext context)
          {
               this.context = context;
               entities = context.Set<T>();
          }
          public IEnumerable<T> GetAll()
          {
               return entities.AsEnumerable();
          }
          public void Insert(T entity)
          {
               if (entity == null)
               {
                    throw new ArgumentNullException("entity");
               }
               entities.Add(entity);
          }
          public void Update(T entity)
          {
               if (entity == null)
               {
                    throw new ArgumentNullException("entity");
               }
               entities.Update(entity);
          }
          public void Delete(T entity)
          {
               if (entity == null)
               {
                    throw new ArgumentNullException("entity");
               }
               entities.Remove(entity);
          }
          public void Save()
          {
               context.SaveChanges();
          }

          public void Dispose()
          {
               context.Dispose();
          }
     }
}
