const API_URL = "https://localhost:7238/api/CollectionEntry";

export async function getCardByPokedexNumber(pokedexNumber) {
    const response = await fetch(`${API_URL}/${pokedexNumber}`, {
        method: "GET",
        headers: {"Content-Type": "application/json"},
    });

    if (!response.ok) throw new Error("Erro ao buscar a carta.");

    return await response.json();
}

export async function addCardToCollection(pokedexNumber, cardData) {
    const response = await fetch(`${API_URL}/${pokedexNumber}`, {
        method: "PUT",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(cardData)
    });
    
    if (!response.ok) throw new Error("Erro ao adicionar carta à coleção.");
}

export async function removeCardFromCollection(pokedexNumber) {
    const response = await fetch(`${API_URL}/${pokedexNumber}`, {
        method: "DELETE",
        headers: {"Content-Type": "application/json"},
    });

    if (!response.ok) throw new Error("Erro ao remover carta da coleção.");
}