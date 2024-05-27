using TokenizationService.API.Repositories;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Repositories;
using TokenizationService.Core.API.Services;

namespace TokenizationService.Core.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IConfigurationRepository<TenantConfiguration>, TenantConfigurationRepository>();
            services.AddScoped<ITokenServiceGenerator, TokenGeneratorService>();
            services.AddScoped<IEngineService, EngineService>();
            services.AddScoped<IEncryptionProvider, EncryptionProvider>();
            services.AddScoped<IConfigurationRepository<TenantConfiguration>, TenantConfigurationRepository>();
            services.AddScoped<IEncryptionService, DesEncryptionService>();
            services.AddScoped<IEncryptionService, RsaEncryptionService>();
            services.AddScoped<IRepository<TokenObject>, MongoTokenRepository>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
