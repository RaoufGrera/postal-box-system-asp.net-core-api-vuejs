using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoxSystem10.Enums;
using BoxSystem10.Model;
using BoxSystem10.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using SystemData;
using SystemData.Models;
using ServiceReference1;
using System.IO;

namespace BoxSystem10
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
   
        public IConfiguration Configuration { get; }
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
       
            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);


            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret));

        services.AddDbContext<SystemContext>(os 
                => os.UseSqlServer(Configuration.GetConnectionString("SystemConnection")));

            services.AddIdentity<AppUser, IdentityRole<int>>()
               .AddEntityFrameworkStores<SystemContext>()
                  

               .AddDefaultTokenProviders();

            services.AddScoped<IService1, Service1Client>();

            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["FacebookAuthSettings:AppId"];
            //    facebookOptions.AppSecret = Configuration["FacebookAuthSettings:AppSecret"];
            //});
            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

            //var authenticationProviderKey = "TestKey";

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = false,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = false,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg => {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = tokenValidationParameters;
                }).AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["FacebookAuthSettings:AppId"];
                    facebookOptions.AppSecret = Configuration["FacebookAuthSettings:AppSecret"];
                }) ;

            // services.AddMvc();
            services.Configure<IdentityOptions>(option => {
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
            });

            services.AddSwaggerGen(options => {
                options.DescribeAllEnumsAsStrings();
                options.DescribeStringEnumsInCamelCase();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "API v1",
                    Version = "v1",
                 });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminGuard",
                    policy => policy.RequireClaim("Admin",EnumUserType.Admin.ToString()));
                options.AddPolicy("OfficeGuard",
                   policy => policy.RequireClaim("Office", EnumUserType.Office.ToString()));
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                      builder =>
                      {
                      builder.WithOrigins("https://localhost:3001")
                      .AllowAnyOrigin()
                             .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                      });
            });

   
            services.AddMvc();

            services.AddScoped<IAuthService, AuthService>();
 

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {

            var con = (SystemContext)serviceProvider.GetService(typeof(SystemContext));
         /*   app.Use(async (context, next) => {
                await next();
                if (context.Response.StatusCode == 404 &&
                   !Path.HasExtension(context.Request.Path.Value) &&
                   !context.Request.Path.Value.StartsWith("/v1/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });*/
            app.UseCors(x => x
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials()
          );

           /* if (con != null) {

                con.Database.EnsureDeleted();

               con.Database.EnsureCreated();
            }*/

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();

           // app.UseHttpsRedirection();
           // app.UseMvc();
            app.UseMvcWithDefaultRoute();
            //    app.UseDefaultFiles();
            //  app.UseStaticFiles();
            app.UseSwagger();

          app.UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HTTP API V1"));



        }
    }
}
