using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Infrastructure.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ExternalId)
            .HasMaxLength(255)
            .IsRequired();
        builder.HasIndex(c => c.ExternalId)
            .IsUnique();

        builder.Property(c => c.PokemonId)
            .IsRequired(false);

        builder.Property(c => c.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.CardNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.Rarity)
            .HasConversion<string>()
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.ImageUrl)
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(c => c.SetName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.SetCode)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(c => c.CollectionEntries)
            .WithOne(ce => ce.Card)
            .HasForeignKey(ce => ce.CardId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
