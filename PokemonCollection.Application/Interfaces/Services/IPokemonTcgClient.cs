using PokemonCollection.Application.DTOs.CardsDtos;

namespace PokemonCollection.Application.Interfaces.Services;

public interface IPokemonTcgClient
{
    Task<CardListResponseDto?> GetCardsAsync(int page, int pageSize);
}
