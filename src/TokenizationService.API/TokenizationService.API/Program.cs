//using AutoMapper;
//using Microsoft.Extensions.DependencyInjection.Extensions;
//using TokenizationService.Configuration.Models;
//using TokenizationService.Configuration.Repository;
//using TokenizationService.Core.API;
//using TokenizationService.Core.API.Services;
//using TokenizationService.Core.API.Utilities;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//#if DEBUG // just for now until i get key vault and configuration stuff up and running
//builder.Configuration.AddJsonFile("local.settings.json");
//#endif

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IConfigurationRepository<TenantConfiguration>, TenantConfigurationRepository>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//var config = new MapperConfiguration(cfg =>
//{
//    cfg.AddProfile<MappingProfile>();
//});


//app.MapControllers();

//app.Run();
using TokenizationService.Core.API;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>(); // Specify the Startup class
            }).ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
            });
}