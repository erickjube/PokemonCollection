using PokemonCollection.Application.DTOs.CardsDtos;
using PokemonCollection.Application.Interfaces.Services;
using System.Net.Http.Json;

namespace PokemonCollection.Infrastructure.ExternalApis;

public class PokemonTcgClient : IPokemonTcgClient
{
    private readonly HttpClient _httpClient;

    public PokemonTcgClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CardListResponseDto?> GetCardsAsync(int page, int pageSize)
    {
        return await _httpClient.GetFromJsonAsync<CardListResponseDto>($"cards?page={page}&pageSize={pageSize}");
    }
}
