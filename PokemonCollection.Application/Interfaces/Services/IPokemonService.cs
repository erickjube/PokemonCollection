using PokemonCollection.Application.DTOs.PokemonsDtos;

namespace PokemonCollection.Application.Interfaces.Services;

public interface IPokemonService
{
    Task<IEnumerable<PokemonResponseDto>> GetAllAsync();
    Task<PokemonResponseDto> GetByIdAsync(int pokemonId);
}
