using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class DogConfiguration : IEntityTypeConfiguration<Dog>
{
    public void Configure(EntityTypeBuilder<Dog> builder)
    {
        builder.Property<int>("id")
            .UseIdentityColumn();

        builder.HasKey("id");

        builder.HasIndex(d => d.Name)
            .IsUnique();

        builder.Property(d => d.TailLength)
            .HasColumnName("tail_length")
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(d => d.Weight)
            .HasColumnName("weight")
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(d => d.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(d => d.Color)
            .HasColumnName("color")
            .IsRequired();
    }
}
