using PokemonCollection.Application.DTOs.CardsDtos;

namespace PokemonCollection.Application.Interfaces.Services;

public interface ICardService
{
    Task<IEnumerable<CardResponseDto>> GetByPokemonIdAsync(int pokemonId);
    Task<CardResponseDto> GetByIdAsync(int cardId);
}
