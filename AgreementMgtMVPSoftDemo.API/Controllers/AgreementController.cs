using AgreementMgtMVPSoftDemo.DAL;
using AgreementMgtMVPSoftDemo.Model;
using Microsoft.AspNetCore.Mvc;
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
          public AgreementController(IGenericRepository<ProductGroup> productGroupRepository
               , IGenericRepository<Product> productRepository
               , IGenericRepository<Agreement> agreementRepository
               , IUserRepository iUserRepository
               , IAgreementRepository userAgreementsrepository)
          {
               _productGroupRepository = productGroupRepository;
               _productRepository = productRepository;
               _agreementRepository = agreementRepository;
               _iUserRepository = iUserRepository;
               _userAgreementsrepository = userAgreementsrepository;
          }

          [HttpPost("GetAgreement")]
          public object GetAgreement(string email)
          {
               var productgroups = _productGroupRepository.GetAll();
               var products = _productRepository.GetAll();


               return new { productgroups, products };
          }

          [HttpGet("GetProductGroup")]
          public IEnumerable<ProductGroup> GetProductGroup()
          {
               return _productGroupRepository.GetAll();
          }
          [HttpPost("GetProduct")]
          public IEnumerable<Product> GetProduct(int groupId)
          {
               return _productRepository.GetAll().Where(t => t.ProductGroupId == groupId);
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
                         case "FirstName":
                              if (sortColumnDir == "desc")
                                   customerData = customerData.OrderByDescending(s => s.FirstName);
                              else
                                   customerData = customerData.OrderBy(s => s.FirstName);
                              break;
                         case "LastName":
                              if (sortColumnDir == "desc")
                                   customerData = customerData.OrderByDescending(s => s.LastName);
                              else
                                   customerData = customerData.OrderBy(s => s.LastName);
                              break;
                         case "Email":
                              if (sortColumnDir == "desc")
                                   customerData = customerData.OrderByDescending(s => s.Email);
                              else
                                   customerData = customerData.OrderBy(s => s.Email);
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
                    customerData = customerData.Where(m => m.FirstName.ToLower().Contains(searchValue.ToLower()));
               }

               //total number of rows count     
               recordsTotal = customerData.Count();
               //Paging     
               var data = customerData.Skip(skip).Take(pageSize).ToList();
               var user = _iUserRepository.Get(email);
               //Returning Json Data    
               return new JsonResult(new DataTableResponse<UserAgreements>()
               {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = customerData,
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
                    EffectiveDate = data.EffectiveDate,
                    ExpirationDate = data.ExpirationDate
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
     }
}
