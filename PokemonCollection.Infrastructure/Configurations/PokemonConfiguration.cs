using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Infrastructure.Configurations;

public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.PokedexNumber)
            .HasColumnType("integer")
            .IsRequired();
        builder.HasIndex(p => p.PokedexNumber)
            .IsUnique();

        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();
        builder.HasIndex(p => p.Name)
            .IsUnique();

        builder.Property(p => p.Generation)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Region)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.PrimaryType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.SecondaryType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(2048)
            .IsRequired();

        builder.HasMany(p => p.Cards)
            .WithOne(c => c.Pokemon)
            .HasForeignKey(c => c.PokemonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
