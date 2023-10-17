using Application;
using AspNetCoreRateLimit;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddWeb(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
    

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger<Program>();
var context = services.GetRequiredService<DogsHouseContext>();

try
{
    await context.Database.MigrateAsync();
    await DogsHouseContextSeed.SeedAsync(context, loggerFactory);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseIpRateLimiting();

app.MapControllers();

app.Run();
