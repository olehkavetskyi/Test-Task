using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DogsHouseContext>(option => 
        option.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDogsService, DogsService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
