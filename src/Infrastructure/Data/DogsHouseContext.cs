using Domain.Entities;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DogsHouseContext : DbContext
{
    public DbSet<Dog> Dogs { get; set; }
    public DogsHouseContext(DbContextOptions options) : base(options)
    { 
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DogConfiguration());
    }
}
