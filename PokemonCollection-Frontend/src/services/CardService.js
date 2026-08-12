const API_URL = "https://localhost:7238/api/CollectionEntry";

export async function getCardByPokedexNumber(pokedexNumber) {
    const response = await fetch(`${API_URL}/${pokedexNumber}`);
    if (!response.ok) throw new Error("Erro ao buscar o card");
    return await response.json();
}