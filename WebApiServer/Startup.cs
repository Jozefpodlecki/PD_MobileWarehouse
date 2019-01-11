using System;
using System.Text;
using Common;
using Common.Services;
using Data_Access_Layer;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApiServer.Managers;
using WebApiServer.Middleware;
using WebApiServer.Providers;

namespace WebApiServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment
            )
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddSingleton<JwtTokenProvider>();

            services.Configure<JwtConfiguration>(Configuration.GetSection(nameof(JwtConfiguration)));

            services.AddTransient(typeof(IRepository<>),typeof(Repository<>));
            services.AddTransient(typeof(INameRepository<>), typeof(NameRepository<>));
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserResolverService, UserResolverService>();
            services.AddTransient<UnitOfWork>();
            services.AddTransient<PasswordManager>();
            services.AddTransient<ProductRepository>();
            services.AddTransient<AttributeRepository>();
            services.AddTransient<DbContext, SiteDbContext>();
            services.AddTransient<UnitOfWork>();
            
            services.AddDbContext<SiteDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SiteDbContext>()
                .AddDefaultTokenProviders();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtConfiguration));
            var signingKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(jwtAppSettingOptions[nameof(JwtConfiguration.Key)]));

            services.Configure<JwtConfiguration>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtConfiguration.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtConfiguration.Audience)];
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtConfiguration.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtConfiguration.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtConfiguration.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            services.AddAuthorization(options =>
            {
                foreach (var item in PolicyTypes.Properties)
                {
                    options.AddPolicy(item, policy =>
                    {
                        policy.RequireClaim(SiteClaimTypes.Permission, item);
                    });
                } 
            });

#if DEBUG
            services
                    .AddMvc(opts =>
                    {
                        opts
                            .Filters
                            .Add(new AllowAnonymousFilter());
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
#else
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
#endif            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SiteDbContext>();
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMvc();
            
        }
    }
}
