using System;
using System.Text;
using Data_Access_Layer;
using Data_Access_Layer.Entity;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApiServer.Constants;
using WebApiServer.Managers;
using WebApiServer.Providers;
using WebApiServer.Services;

namespace WebApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddSingleton<JwtTokenProvider>();

            services.Configure<JwtConfiguration>(Configuration.GetSection(nameof(JwtConfiguration)));

            services.AddTransient(typeof(IRepository<>),typeof(Repository<>));
            services.AddTransient(typeof(INameRepository<>), typeof(NameRepository<>));
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<LogService>();
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
                options.AddPolicy(PolicyTypes.Users.Add, policy =>
                {
                    policy.RequireClaim(SiteClaimTypes.Permission, PolicyTypes.Users.Add);
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                //context.Database.OpenConnection();
                //var connection = context.Database.GetDbConnection();
                //var command = connection.CreateCommand();
                //command.CommandText = File.ReadAllText(@"C:\Users\Admin\Documents\Visual Studio 2017\Projects\Praca Dyplomowa\Data Access Layer\Seed\Init.sql");
                //command.ExecuteNonQuery();


            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
