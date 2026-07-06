using PokemonCollection.Domain.ENUMs;
using System.Globalization;

namespace PokemonCollection.Domain.Entities;

public class Card
{
    public int Id { get; private set; }
    public string ExternalId { get; private set; } = string.Empty;

    public int PokemonId { get; private set; }
    public Pokemon Pokemon { get; private set; }

    public string Name { get; private set; } = string.Empty;
    public RarityCard Rarity { get ; private set; } = RarityCard.Common;
    public string ImageUrl { get; private set; } = string.Empty;
    public string SetName { get; private set; } = string.Empty; // nome da coleção da carta (151, Coroa Estelar, etc...)
    public ICollection<CollectionEntry> CollectionEntries { get; private set; }

    private Card() { }
    public Card(string externalId, int pokemonId, string name, RarityCard rarity, string imageUrl, string setName)
    {
        if (string.IsNullOrWhiteSpace(externalId)) throw new ArgumentException("Id externo é obrigatório", nameof(externalId));
        if (pokemonId <= 0) throw new ArgumentException("PokemonId deve ser positivo", nameof(pokemonId));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome é obrigatório", nameof(name));
        if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Imagem Url é obrigatória", nameof(imageUrl));
        if (string.IsNullOrWhiteSpace(setName)) throw new ArgumentException("Nome da Coleção é obrigatório", nameof(setName));

        ExternalId = externalId;
        PokemonId = pokemonId;
        Name = name;
        Rarity = rarity;
        ImageUrl = imageUrl;
        SetName = setName;
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
