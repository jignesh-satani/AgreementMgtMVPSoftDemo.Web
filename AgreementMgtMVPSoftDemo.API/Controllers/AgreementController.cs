using AgreementMgtMVPSoftDemo.DAL;
using AgreementMgtMVPSoftDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgreementMgtMVPSoftDemo.API.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class AgreementController : ControllerBase
     {
          private readonly IGenericRepository<ProductGroup> _productGroupRepository;
          private readonly IGenericRepository<Product> _productRepository;
          private readonly IGenericRepository<Agreement> _agreementRepository;
          private readonly IAgreementRepository _userAgreementsrepository;
          private readonly IUserRepository _iUserRepository;
          private readonly IMemoryCache _memoryCache;
          //private readonly MemoryCache _iCacheHelper;

          public AgreementController(IGenericRepository<ProductGroup> productGroupRepository
               , IGenericRepository<Product> productRepository
               , IGenericRepository<Agreement> agreementRepository
               , IUserRepository iUserRepository
               , IAgreementRepository userAgreementsrepository
               , IMemoryCache memoryCache
               , ICacheHelper ICacheHelper)
          {
               _productGroupRepository = productGroupRepository;
               _productRepository = productRepository;
               _agreementRepository = agreementRepository;
               _iUserRepository = iUserRepository;
               _userAgreementsrepository = userAgreementsrepository;
               _memoryCache = memoryCache;
               //_iCacheHelper = ICacheHelper;

          }

          //[HttpPost("GetAgreement")]
          //public object GetAgreement(string email)
          //{
          //     var productgroups = _productGroupRepository.GetAll();
          //     var products = _productRepository.GetAll();


          //     return new { productgroups, products };
          //}

          [HttpGet("GetProductGroup")]
          public IEnumerable<ProductGroup> GetProductGroup()
          {
          //     var cacheKey = "productGroupList";
          //     if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<ProductGroup> productGroupList))
          //     {
          //          //calling the server
          //          productGroupList = _productGroupRepository.GetAll();

          //          //setting up cache options
          //          //var cacheExpiryOptions = new MemoryCacheEntryOptions
          //          //{
          //          //     AbsoluteExpiration = DateTime.Now.AddSeconds(50),
          //          //     Priority = CacheItemPriority.High,
          //          //     SlidingExpiration = TimeSpan.FromSeconds(20)
          //          //};
          //          var cacheExpiryOptions = new MemoryCacheEntryOptions()
          //.SetSlidingExpiration(TimeSpan.FromSeconds(3600));
          //          _memoryCache.Set(cacheKey, productGroupList, cacheExpiryOptions);
          //     }
          //     return productGroupList;

               return _productGroupRepository.GetAll();
               //return _iCacheHelper.GetProductGroup(_memoryCache, _productGroupRepository);
          }
          [HttpPost("GetProduct")]
          public IEnumerable<Product> GetProduct(int groupId)
          {
               //   var cacheKey = "productList";
               //   if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Product> productList))
               //   {
               //        //calling the server
               //        productList = _productRepository.GetAll();

               //        //setting up cache options
               //        //var cacheExpiryOptions = new MemoryCacheEntryOptions
               //        //{
               //        //     AbsoluteExpiration = DateTime.Now.AddSeconds(50),
               //        //     Priority = CacheItemPriority.High,
               //        //     SlidingExpiration = TimeSpan.FromSeconds(20)
               //        //};
               //        var cacheExpiryOptions = new MemoryCacheEntryOptions()
               //.SetSlidingExpiration(TimeSpan.FromSeconds(3600));
               //        _memoryCache.Set(cacheKey, productList, cacheExpiryOptions);
               //   }
               //   return productList;
               return _productRepository.GetAll().Where(t => t.ProductGroupId == groupId);
               //return _iCacheHelper.GetProducts(_memoryCache, _productRepository).Where(t => t.ProductGroupId == groupId);
          }

          [HttpPost("LoadGrid")]
          public IActionResult LoadGrid(string email)
          {
               var agreements = _userAgreementsrepository.GetUserAgreements(email);

               var draw = Request.Form["draw"].FirstOrDefault();
               var start = Request.Form["start"].FirstOrDefault();
               var length = Request.Form["length"].FirstOrDefault();
               var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

               var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
               var searchValue = Request.Form["search[value]"].FirstOrDefault();

               //Paging Size (10,20,50,100)    
               int pageSize = length != null ? Convert.ToInt32(length) : 0;
               int skip = start != null ? Convert.ToInt32(start) : 0;
               int recordsTotal = 0;

               // Getting all Customer data    
               var customerData = (from tempcustomer in agreements
                                   select tempcustomer);

               //Sorting    
               if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
               {
                    switch (sortColumn)
                    {
                         case "ProductPrice":
                              if (sortColumnDir == "desc")
                                   customerData = customerData.OrderByDescending(s => s.ProductPrice);
                              else
                                   customerData = customerData.OrderBy(s => s.ProductPrice);
                              break;
                         case "NewPrice":
                              if (sortColumnDir == "desc")
                                   customerData = customerData.OrderByDescending(s => s.NewPrice);
                              else
                                   customerData = customerData.OrderBy(s => s.NewPrice);
                              break;
                         case "ProductDescription":
                              if (sortColumnDir == "desc")
                                   customerData = customerData.OrderByDescending(s => s.ProductDescription);
                              else
                                   customerData = customerData.OrderBy(s => s.ProductDescription);
                              break;
                         case "GroupDescription":
                              if (sortColumnDir == "desc")
                                   customerData = customerData.OrderByDescending(s => s.GroupDescription);
                              else
                                   customerData = customerData.OrderBy(s => s.GroupDescription);
                              break;
                    }

               }
               //Search    
               if (!string.IsNullOrEmpty(searchValue))
               {
                    customerData = customerData.Where(m => m.ProductDescription.ToLower().Contains(searchValue.ToLower()));
               }

               //total number of rows count     

               //Paging
               recordsTotal = customerData.Count();
               var data = customerData.Skip(skip).Take(pageSize).ToList();

               var user = _iUserRepository.Get(email);
               //Returning Json Data    
               return new JsonResult(new DataTableResponse<UserAgreements>()
               {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = data,
                    aspNetUser = user
               });
          }

          [HttpPost("SaveAgreement")]
          public IActionResult SaveAgreement(UserAgreements data)
          {
               Agreement obj = new Agreement()
               {
                    Id = data.Id,
                    UserId = data.UserId,
                    ProductGroupId = data.ProductGroupId,
                    ProductId = data.ProductId,
                    NewPrice = data.NewPrice,
                    EffectiveDate = data.EffectiveDate.ToDateTime(),
                    ExpirationDate = data.ExpirationDate.ToDateTime()
               };
               if (data.Id > 0)
                    _agreementRepository.Update(obj);
               else
                    _agreementRepository.Insert(obj);

               _agreementRepository.Save();
               _agreementRepository.Dispose();
               return new JsonResult(new TokenResponse
               {
                    Message = "Save",
                    Success = true
               });
          }

          [HttpPost("DeleteAgreement")]
          public IActionResult DeleteAgreement(int id)
          {
               _agreementRepository.Delete("Delete From Agreement Where Id = " + id.ToString());
               _agreementRepository.Dispose();
               return new JsonResult(new TokenResponse
               {
                    Message = "Record Deleted",
                    Success = true
               });
          }
     }
}
