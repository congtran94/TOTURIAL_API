using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessService;
using BusinessService.Interface;
using GOSDataModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using TOTURIAL_API.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace TOTURIAL_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            var secret = services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var mappingConfig = new AutoMapper.MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            AutoMapper.IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<ISendEmail, SendEmail>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddDbContext<GOSContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddControllers();
            //services.AddDefaultIdentity<IdentityUser>(
            //     options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<GOSContext>();
            //services.addde<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<GOSContext>();

            var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Secret", "MIIBljCCAUACCQCIDMpqK7WfWDANBgkqhkiG9w0BAQsFADBSMQswCQYDVQQGEwJVUzETMBEGA1UECAwKU29tZS1TdGF0ZTESMBAGA1UECgwJTHV4b3R0aWNhMRowGAYDVQQLDBFMdXhvdHRpY2EgZXllY2FyZTAeFw0xODA1MjMxNTE1MjdaFw0yODA1MjAxNTE1MjdaMFIxCzAJBgNVBAYTAlVTMRMwEQYDVQQIDApTb21lLVN0YXRlMRIwEAYDVQQKDAlMdXhvdHRpY2ExGjAYBgNVBAsMEUx1eG90dGljYSBleWVjYXJlMFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAKuMYcirPj81WBtMituJJenF0CG/HYLcAUOtWKl1HchC0dM8VRRBI/HV+nZcweXzpjhX8ySa9s7kJneP0cuJiU8CAwEAATANBgkqhkiG9w0BAQsFAANBAKEM8wQwlqKgkfqnNFcbsZM0RUxS+eWR9LvycGuMN7aL9M6GOmfpQmF4MH4uvkaiZenqCkhDkyi4Cy81tz453tQ=")));
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    // Clock skew compensates for server time drift.
                    // We recommend 5 minutes or less:
                    ClockSkew = TimeSpan.FromMinutes(5),
                    // Specify the key used to sign the token:
                    IssuerSigningKey = secretKey,
                    RequireSignedTokens = true,
                    // Ensure the token hasn't expired:
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    // Ensure the token audience matches our audience value (default true):
                    ValidateAudience = true,
                    ValidAudience = "api://default",
                    // Ensure the token was issued by a trusted authorization server (default true):
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:14065/api/Account/authenticate"
                };
            });

            //services.Configure<IdentityOptions>(option =>
            //{
            //    //Password setting
            //    option.Password.RequireDigit = true;
            //    option.Password.RequireLowercase = true;
            //    option.Password.RequireNonAlphanumeric = true;
            //    option.Password.RequireUppercase = true;
            //    option.Password.RequiredLength = 8;
            //    option.Password.RequiredUniqueChars = 1;
            //    //lockout setting 
            //    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    option.Lockout.MaxFailedAccessAttempts = 5;
            //    option.Lockout.AllowedForNewUsers = true;
            //    //user setting 
            //    option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    option.User.RequireUniqueEmail = true;

            //});
            //services.ConfigureApplicationCookie(option =>
            //{
            //    option.Cookie.HttpOnly = true;
            //    option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //    option.LoginPath = "/Identity/Account/Login";
            //    option.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    option.SlidingExpiration = true;
            //});
            //services.AddMvc(option => option.EnableEndpointRouting = true).SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddCors(options => {
                options.AddPolicy("AllowOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            //services.AddSingleton<IAuthorizationHandler>
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseCors("AllowOrigins");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action}/{id?}");
                //endpoints.MapControllerRoute(
                //   name: "aciton",
                //   pattern: "{controller}/{action}/{id?}");
            });
            //app.UseMvc();
        }
    }
}
