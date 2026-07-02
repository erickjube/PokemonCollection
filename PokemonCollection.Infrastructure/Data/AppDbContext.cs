using Microsoft.EntityFrameworkCore;
using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<CollectionEntry> CollectionEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
