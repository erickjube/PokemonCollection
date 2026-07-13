using PokemonCollection.Domain.Entities;

namespace PokemonCollection.Application.Interfaces.Repositories;

public interface IImportStateRepository
{
    Task<ImportState?> GetAsync();
    Task AddAsync(ImportState state);
}
