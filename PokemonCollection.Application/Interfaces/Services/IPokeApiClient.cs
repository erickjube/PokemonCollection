using PokemonCollection.Application.DTOs.PokemonsDtos;

namespace PokemonCollection.Application.Interfaces.Services;

public interface IPokeApiClient
{
    Task<PokemonListResponseDto?> GetAllPokemonAsync(int offset, int limit);

    Task<PokemonResponseDto?> GetPokemonAsync(int id);

    Task<PokemonSpeciesResponseDto?> GetSpeciesAsync(int id);
}
