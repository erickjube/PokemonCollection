using PokemonCollection.Application.DTOs.CardsDtos;
using PokemonCollection.Application.Interfaces.Services;
using System.Net.Http.Json;
using System.Text.Json;

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
        var url = $"cards?page={page}&pageSize={pageSize}";
        Console.WriteLine($"Chamando: {url}");

        var response = await _httpClient.GetAsync(url);
        Console.WriteLine($"Status: {(int)response.StatusCode} - {response.StatusCode}");

        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
        response.EnsureSuccessStatusCode();

        return System.Text.Json.JsonSerializer.Deserialize<CardListResponseDto>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        //return await _httpClient.GetFromJsonAsync<CardListResponseDto>(url);
    }
}
