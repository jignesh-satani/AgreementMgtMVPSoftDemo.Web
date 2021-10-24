using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public class GenericRepository<T>: IGenericRepository<T> where T:class
     {
          private readonly ApplicationContext context;
          private readonly DbSet<T> entities;
          
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
          public void Delete(string sql)
          {
               context.Database.ExecuteSqlRaw(sql);
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
