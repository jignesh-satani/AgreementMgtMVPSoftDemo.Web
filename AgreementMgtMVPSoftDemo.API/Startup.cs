using AgreementMgtMVPSoftDemo.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.EntityFrameworkCore;
using AgreementMgtMVPSoftDemo.API.Middleware;
using AgreementMgtMVPSoftDemo.API.Auth;
using AgreementMgtMVPSoftDemo.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
               //services.AddMvc();
               services.AddMemoryCache();
               services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));
               //services.AddSingleton<IJwtAuth>(new JwtAuth(key));
               services.Configure<AppSettings>(_config.GetSection("JwtConfig"));
               services.AddScoped<IJwtAuth, JwtAuth>();               
               services.AddTransient<IUserRepository, UserRepository>();
               services.AddTransient<IGenericRepository<Product>, GenericRepository<Product>>();
               services.AddTransient<IGenericRepository<ProductGroup>, GenericRepository<ProductGroup>>();
               services.AddTransient<IGenericRepository<Agreement>, GenericRepository<Agreement>>();
               services.AddTransient<IAgreementRepository, AgreementRepository>();
               services.AddTransient<ILoggerManager, LoggerManager>();
               services.AddSingleton<ICacheHelper, CacheHelper>();

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
               //services.AddTokenAuthentication(_config);
               //services.AddSwaggerGen(c =>
               //{
               //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agreement Management API", Version = "v1" });
               //});
               #region Swagger Configuration
               services.AddSwaggerGen(swagger =>
               {
                    //This is to generate the Default UI of Swagger Documentation
                    swagger.SwaggerDoc("v1", new OpenApiInfo
                    {
                         Version = "v1",
                         Title = "Agreement Management API",
                         Description = "ASP.NET Core 5.0 Web API"
                    });
                    // To Enable authorization using Swagger (JWT)
                    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                         Name = "Authorization",
                         Type = SecuritySchemeType.ApiKey,
                         Scheme = "Bearer",
                         BearerFormat = "JWT",
                         In = ParameterLocation.Header,
                         Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    });
                    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
               });
               #endregion
               #region Authentication
               services.AddAuthentication(option =>
               {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

               }).AddJwtBearer(options =>
               {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = false,
                         ValidateIssuerSigningKey = true,
                         //ValidIssuer = Configuration["Jwt:Issuer"],
                         //ValidAudience = Configuration["Jwt:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) //Configuration["JwtToken:SecretKey"]
                    };
               });
               #endregion
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
               
               app.ConfigureCustomExceptionMiddleware();
               app.UseRouting();
               app.UseAuthentication();
               app.UseAuthorization();
               app.UseSwagger();
               app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Agreement Management"));
               app.UseMiddleware<JwtMiddleware>();
               app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    
          }
     }
}
