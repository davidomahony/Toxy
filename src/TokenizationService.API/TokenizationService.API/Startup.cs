using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Handlers;
using TokenizationService.Core.API.Repositories;
using TokenizationService.Core.API.Services;
using TokenizationService.Core.API.Services.EncryptionServices;

namespace TokenizationService.Core.API
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ITokenServiceGenerator, TokenGeneratorService>();
            services.AddScoped<IEngineService, EngineService>();
            services.AddScoped<IEncryptionProvider, EncryptionProvider>();
            services.AddScoped<ITokenParser, TokenParser>();
            services.AddScoped<IEncryptionService, DesEncryptionService>();
            services.AddScoped<IEncryptionService, RsaEncryptionService>();
            services.AddScoped<IEncryptionService, AesEncryptionService>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            this.RegisterRepositories(services);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
              {
                  options.Audience = "https://localhost:7160/";
                  options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                  options.Audience = configuration["Auth0:Audience"];
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      NameClaimType = ClaimTypes.NameIdentifier
                  };
              });

            services
              .AddAuthorization(options =>
              {
                  options.AddPolicy(
                    "read:messages",
                    policy => policy.Requirements.Add(
                      new PrivilegeIdentifier("read:messages", configuration["Auth0:Domain"])
                    )
                  );
              });

            services.AddSingleton<IAuthorizationHandler, AuthorizationPrivilegeHandler>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        protected virtual void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IConfigurationRepository<TenantConfiguration>, TenantConfigurationRepository>();
            services.AddScoped<IGenericTokenRepository, MongoGenericStringTokenRepository>();
        }
    }
}
