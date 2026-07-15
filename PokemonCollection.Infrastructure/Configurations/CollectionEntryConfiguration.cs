using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Infrastructure.Configurations;

public class CollectionEntryConfiguration : IEntityTypeConfiguration<CollectionEntry>
{
    public void Configure(EntityTypeBuilder<CollectionEntry> builder)
    {
        builder.HasKey(ce => ce.Id);

        builder.HasIndex(ce => ce.CardId)
            .IsUnique();

        builder.HasIndex(ce => ce.PokemonId)
            .IsUnique();

        builder.Property(ce => ce.Condition)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(ce => ce.Language)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(ce => ce.Extra)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(ce => ce.DateAdded)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();
    }
}
