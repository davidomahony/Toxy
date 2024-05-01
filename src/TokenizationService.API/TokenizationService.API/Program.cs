using Microsoft.Extensions.DependencyInjection.Extensions;
using TokenizationService.Configuration.Models;
using TokenizationService.Configuration.Repository;
using TokenizationService.Core.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#if DEBUG // just for now until i get key vault and configuration stuff up and running
builder.Configuration.AddJsonFile("local.settings.json");
#endif

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IConfigurationRepository<TenantConfiguration>, TenantConfigurationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
