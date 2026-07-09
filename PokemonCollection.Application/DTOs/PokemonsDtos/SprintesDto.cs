using System.Text.Json.Serialization;

namespace PokemonCollection.Application.DTOs.PokemonsDtos;

public class SpritesDto
{
    [JsonPropertyName("other")]
    public OtherSpritesDto Other { get; set; } = null!;
}

public class OtherSpritesDto
{
    [JsonPropertyName("official-artwork")]
    public OfficialArtworkDto OfficialArtwork { get; set; } = null!;
}

public class OfficialArtworkDto
{
    [JsonPropertyName("front_default")]
    public string FrontDefault { get; set; } = string.Empty;
}