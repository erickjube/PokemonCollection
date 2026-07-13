using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Infrastructure.Configurations;

public class ImportStateConfiguration : IEntityTypeConfiguration<ImportState>
{
    public void Configure(EntityTypeBuilder<ImportState> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.LastImportedPage)
            .IsRequired();

        builder.Property(i => i.LastExecution)
            .IsRequired();
    }
}
