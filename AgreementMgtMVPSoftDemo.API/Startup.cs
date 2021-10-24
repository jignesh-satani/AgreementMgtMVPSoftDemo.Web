using AgreementMgtMVPSoftDemo.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgreementMgtMVPSoftDemo.API.Middleware;
using AgreementMgtMVPSoftDemo.API.Auth;
using AgreementMgtMVPSoftDemo.Model;

namespace AgreementMgtMVPSoftDemo.API
{
     public class Startup
     {
          public IConfiguration _config { get; }
          public Startup(IConfiguration configuration)
          {
               _config = configuration;
          }

          // This method gets called by the runtime. Use this method to add services to the container.
          public void ConfigureServices(IServiceCollection services)
          {
               //services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
               //{
               //     //builder.AllowAnyOrigin()
               //     //       .AllowAnyMethod()
               //     //       .AllowAnyHeader();
               //     builder.WithOrigins("https://localhost:44305").AllowAnyMethod().AllowAnyHeader();
               //}));
               var key = _config.GetValue<string>("JwtConfig:secret");

               services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
               services.AddSingleton<IJwtAuth>(new JwtAuth(key));
               services.AddTransient<IUserRepository, UserRepository>();
               services.AddTransient<IGenericRepository<Product>, GenericRepository<Product>>();               
               services.AddTransient<IGenericRepository<ProductGroup>, GenericRepository<ProductGroup>>();
               services.AddTransient<IGenericRepository<Agreement>, GenericRepository<Agreement>>();
               services.AddTransient<IAgreementRepository, AgreementRepository>();

               services.AddControllers().AddNewtonsoftJson();
               services.AddDistributedMemoryCache();
               services.AddSession(options =>
               {
                    options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
               });

               //services.AddControllers();
               services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);
               services.AddTokenAuthentication(_config);
               services.AddSwaggerGen(c =>
               {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agreement Management API", Version = "v1" });
               });
          }

          // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
          {
               if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
               app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials  
               app.UseHttpsRedirection();
               app.UseRouting();
               app.ConfigureCustomExceptionMiddleware();
               app.UseAuthorization();
               app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
               app.UseSwagger();
               app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Agreement Management"));
          }
     }
}
