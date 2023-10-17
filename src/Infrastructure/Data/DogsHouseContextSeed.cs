using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;

namespace Infrastructure.Data;

public class DogsHouseContextSeed
{
    public static async Task SeedAsync(DogsHouseContext context, ILoggerFactory loggerFactory)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        try
        {
            if (!(await context.Dogs.AnyAsync()))
            {
                var dogsData = File.ReadAllText(path + @"/../../../../Infrastructure/Data/SeedData/dogs.json");

                var dogs = JsonSerializer.Deserialize<List<Dog>>(dogsData);

                context.Dogs.AddRange(dogs!);

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<DogsHouseContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}
