namespace PokemonCollection.Application.DTOs.PokemonsDtos;

public class SpritesDto
{
    public OtherSpritesDto Other { get; set; } = null!;
}

public class OtherSpritesDto
{
    public OfficialArtworkDto OfficialArtwork { get; set; } = null!;
}

public class OfficialArtworkDto
{
    public string FrontDefault { get; set; } = string.Empty;
}