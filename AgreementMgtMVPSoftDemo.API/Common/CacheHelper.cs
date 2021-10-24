
using AgreementMgtMVPSoftDemo.DAL;
using AgreementMgtMVPSoftDemo.Model;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace AgreementMgtMVPSoftDemo.API
{
     public interface ICacheHelper
     {
          IEnumerable<ProductGroup> GetProductGroup(IMemoryCache _memoryCache
               , IGenericRepository<ProductGroup> _productGroupRepository);

          IEnumerable<Product> GetProducts(IMemoryCache _memoryCache
              , IGenericRepository<Product> _productRepository);
     }
     public class CacheHelper: ICacheHelper
     {
          public IEnumerable<ProductGroup> GetProductGroup(IMemoryCache _memoryCache
               , IGenericRepository<ProductGroup> _productGroupRepository)
          {
               var cacheKey = "productGroupList";
               if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<ProductGroup> productGroupList))
               {
                    //calling the server
                    productGroupList = _productGroupRepository.GetAll();

                    //setting up cache options
                    var cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                         AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                         Priority = CacheItemPriority.High,
                         SlidingExpiration = TimeSpan.FromSeconds(20)
                    };
                    _memoryCache.Set(cacheKey, productGroupList, cacheExpiryOptions);
               }
               return productGroupList;
          }
          public IEnumerable<Product> GetProducts(IMemoryCache _memoryCache
               , IGenericRepository<Product> _productRepository)
          {
               var cacheKey = "productList";
               if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Product> productList))
               {
                    //calling the server
                    productList = _productRepository.GetAll();

                    //setting up cache options
                    var cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                         AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                         Priority = CacheItemPriority.High,
                         SlidingExpiration = TimeSpan.FromSeconds(20)
                    };
                    _memoryCache.Set(cacheKey, productList, cacheExpiryOptions);
               }
               return productList;
          }
     }
}
