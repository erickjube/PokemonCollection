using PokemonCollection.Application.DTOs.PokemonsDtos;
using PokemonCollection.Application.Interfaces.Services;
using System.Net.Http.Json;

namespace PokemonCollection.Infrastructure.ExternalApis;

public class PokeApiClient : IPokeApiClient
{
    private readonly HttpClient _httpClient;

    public PokeApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PokemonListResponseDto?> GetAllPokemonAsync(int offset, int limit)
    {
        return await _httpClient.GetFromJsonAsync<PokemonListResponseDto>($"pokemon?offset={offset}&limit={limit}");
    }

    public async Task<PokemonResponseDto?> GetPokemonAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<PokemonResponseDto>($"pokemon/{id}");
    }

    public async Task<PokemonSpeciesResponseDto?> GetSpeciesAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<PokemonSpeciesResponseDto>($"pokemon-species/{id}");
    }
}
