namespace PokemonCollection.Domain.Entities;

public class Pokemon
{
    public int Id { get; private set; }
    public int PokedexNumber { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Generation { get; private set; } = string.Empty;
    public string Region { get; private set; } = string.Empty;  
    public string PrimaryType { get; private set; } = string.Empty;
    public string? SecondaryType { get; private set; }
    public string ImageUrl { get; private set; } = string.Empty;
    public CollectionEntry? CollectionEntry { get; private set; }
    public ICollection<Card> Cards { get; private set; } = new List<Card>();

    public Pokemon() { }
    public Pokemon(int pokedexNumber, 
                   string name, 
                   string generation, 
                   string region, 
                   string primaryType, 
                   string? secondaryType, 
                   string imageUrl)
    {
        if (pokedexNumber <= 0) throw new ArgumentException("Numero da Pokédex deve ser positivo", nameof(pokedexNumber));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Nome é obrigatório", nameof(name));
        if (string.IsNullOrWhiteSpace(generation)) throw new ArgumentNullException("Geração é obrigatório", nameof(generation));
        if (string.IsNullOrWhiteSpace(region)) throw new ArgumentNullException("Região é obrigatório", nameof(region));
        if (string.IsNullOrWhiteSpace(primaryType)) throw new ArgumentNullException("Tipo Principal é obrigatório", nameof(primaryType));
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentNullException("Imagem Url é obrigatório", nameof(imageUrl));

        PokedexNumber = pokedexNumber;
        Name = name;
        Generation = generation;
        Region = region;
        PrimaryType = primaryType;
        SecondaryType = secondaryType;
        ImageUrl = imageUrl;
    }

    public void Update(string name, string generation, string region, string primaryType, string? secondaryType, string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Nome é obrigatório", nameof(name));
        if (string.IsNullOrWhiteSpace(generation)) throw new ArgumentNullException("Geração é obrigatório", nameof(generation));
        if (string.IsNullOrWhiteSpace(region)) throw new ArgumentNullException("Região é obrigatório", nameof(region));
        if (string.IsNullOrWhiteSpace(primaryType)) throw new ArgumentNullException("Tipo Principal é obrigatório", nameof(primaryType));
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentNullException("Imagem Url é obrigatório", nameof(imageUrl));

        Name = name;
        Generation = generation;
        Region = region;
        PrimaryType = primaryType;
        SecondaryType = secondaryType;
        ImageUrl = imageUrl;
    }


}
