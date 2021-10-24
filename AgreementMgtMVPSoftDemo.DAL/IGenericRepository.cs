using System;
using System.Collections.Generic;
using System.Text;

namespace AgreementMgtMVPSoftDemo.DAL
{
     public interface IGenericRepository<T> where T : class
     {
          IEnumerable<T> GetAll();
          void Insert(T entity);
          void Update(T entity);
          void Delete(string sql);
          void Dispose();
          void Save();
     }
}
