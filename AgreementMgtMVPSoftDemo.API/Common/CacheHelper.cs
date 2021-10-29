
using AgreementMgtMVPSoftDemo.DAL;
using AgreementMgtMVPSoftDemo.Model;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgreementMgtMVPSoftDemo.API
{
     public interface ICacheHelper
     {
          IEnumerable<ProductGroup> GetProductGroup(IMemoryCache _memoryCache
               , IGenericRepository<ProductGroup> _productGroupRepository);

          IEnumerable<Product> GetProducts(IMemoryCache _memoryCache
              , IGenericRepository<Product> _productRepository);
     }
     public class CacheHelper : ICacheHelper
     {
          private readonly string _productGroupList = "productGroupList";
          private readonly string _productList = "productList";

          public IEnumerable<ProductGroup> GetProductGroup(IMemoryCache _memoryCache
               , IGenericRepository<ProductGroup> _productGroupRepository)
          {
               if (!_memoryCache.TryGetValue(_productGroupList, out IEnumerable<ProductGroup> productGroupList))
               {
                    //calling the server
                    productGroupList = _productGroupRepository.GetAll();

                    //setting up cache options
                    var cacheExpiryOptions = new MemoryCacheEntryOptions()
                                             .SetSlidingExpiration(TimeSpan.FromSeconds(3600));
                    _memoryCache.Set(_productGroupList, productGroupList.ToList(), cacheExpiryOptions);
               }
               return productGroupList;
          }
          public IEnumerable<Product> GetProducts(IMemoryCache _memoryCache
               , IGenericRepository<Product> _productRepository)
          {
               if (!_memoryCache.TryGetValue(_productList, out IEnumerable<Product> productList))
               {
                    //calling the server
                    productList = _productRepository.GetAll();

                    //setting up cache options
                    var cacheExpiryOptions = new MemoryCacheEntryOptions()
                                                  .SetSlidingExpiration(TimeSpan.FromSeconds(3600));
                    _memoryCache.Set(_productList, productList.ToList(), cacheExpiryOptions);
               }
               return productList;
          }
     }
}
