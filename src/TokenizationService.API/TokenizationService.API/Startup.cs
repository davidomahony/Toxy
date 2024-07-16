using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://sts.windows.net/d8e58558-f723-4498-847d-a4f6802171a5/"; // replace with your authority URL
                    options.Audience = "api://c44f4f2a-9018-4dac-a6d4-5bc46aa619a8"; // replace with your audience URL
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://sts.windows.net/d8e58558-f723-4498-847d-a4f6802171a5/", // replace with your issuer URL
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-signing-key")), // replace with your signing key
                        ValidateLifetime = true
                    };
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
