using PokemonCollection.Application.DTOs.PokemonsDtos;
using PokemonCollection.Application.Pagination;
using PokemonCollection.Domain.Common;

namespace PokemonCollection.Application.Interfaces.Services;

public interface IPokemonService
{
    Task<PagedList<PokemonResponseDto>> GetAllAsync(QueryParameters parameters);
    Task<PokemonResponseDto> GetByIdAsync(int pokemonId);
}
