using PokemonCollection.Domain.ENUMs;

namespace PokemonCollection.Domain.Entities;

public class Card
{
    public int Id { get; private set; }
    public string ExternalId { get; private set; } = string.Empty;

    public int? PokemonId { get; private set; }
    public Pokemon? Pokemon { get; private set; }

    public string Name { get; private set; } = string.Empty;
    public string CardNumber { get; private set; } = string.Empty; 
    public RarityCard Rarity { get ; private set; } = RarityCard.Common;
    public string ImageUrl { get; private set; } = string.Empty;
    public string SetName { get; private set; } = string.Empty; // nome da coleção da carta (151, Coroa Estelar, etc...)
    public string SetCode { get; private set; } = string.Empty; // sv3pt5
    public ICollection<CollectionEntry> CollectionEntries { get; private set; } = [];

    private Card() { }
    public Card(string externalId, int? pokemonId, string name, string cardNumber, RarityCard rarity, string imageUrl, string setName, string setCode)
    {
        if (string.IsNullOrWhiteSpace(externalId)) throw new ArgumentException("Id externo é obrigatório", nameof(externalId));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome é obrigatório", nameof(name));
        if (string.IsNullOrWhiteSpace(cardNumber)) throw new ArgumentException("Número da Carta é obrigatório", nameof(cardNumber));
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Imagem Url é obrigatória", nameof(imageUrl));
        if (string.IsNullOrWhiteSpace(setName)) throw new ArgumentException("Nome da Coleção é obrigatório", nameof(setName));
        if (string.IsNullOrWhiteSpace(setCode)) throw new ArgumentException("Código da Coleção é obrigatório", nameof(setCode));

        ExternalId = externalId;
        PokemonId = pokemonId;
        Name = name;
        CardNumber = cardNumber;
        Rarity = rarity;
        ImageUrl = imageUrl;
        SetName = setName;
        SetCode = setCode;
    }

    public void Update(string name, RarityCard rarity, string imageUrl, string setName)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome é obrigatório", nameof(name));
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Imagem Url é obrigatória", nameof(imageUrl));
        if (string.IsNullOrWhiteSpace(setName)) throw new ArgumentException("Nome da Coleção é obrigatório", nameof(setName));

        Name = name;
        Rarity = rarity;
        ImageUrl = imageUrl;
        SetName = setName;
    }

}
