using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Domain.Entities;

public class Pokemon
{
    public int Id { get; private set; }
    public int PokedexNumber { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Generations Generation { get; private set; } 
    public Regions Region { get; private set; } 
    public TypesPokemon PrimaryType { get; private set; } 
    public TypesPokemon? SecondaryType { get; private set; }
    public string ImageUrl { get; private set; } = string.Empty;
    public ICollection<Card> Cards { get; private set; } = new List<Card>();

    private Pokemon() { }
    public Pokemon(int pokedexNumber, 
                   string name, 
                   Generations generation, 
                   Regions region,
                   TypesPokemon primaryType,
                   TypesPokemon? secondaryType, 
                   string imageUrl)
    {
        if (pokedexNumber <= 0) throw new ArgumentException("Numero da Pokédex deve ser positivo", nameof(pokedexNumber));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Nome é obrigatório", nameof(name));
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentNullException("Imagem Url é obrigatório", nameof(imageUrl));

        PokedexNumber = pokedexNumber;
        Name = name;
        Generation = generation;
        Region = region;
        PrimaryType = primaryType;
        SecondaryType = secondaryType;
        ImageUrl = imageUrl;
    }

    public void Update(string name, Generations generation, Regions region, TypesPokemon primaryType, TypesPokemon? secondaryType, string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Nome é obrigatório", nameof(name));
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentNullException("Imagem Url é obrigatório", nameof(imageUrl));

        Name = name;
        Generation = generation;
        Region = region;
        PrimaryType = primaryType;
        SecondaryType = secondaryType;
        ImageUrl = imageUrl;
    }


}
