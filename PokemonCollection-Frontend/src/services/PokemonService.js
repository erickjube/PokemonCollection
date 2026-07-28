export async function getPokemons() {
    
    const response = await fetch("https://localhost:7238/api/Pokemon");

    if (!response.ok) throw new Error("Erro ao buscar os pokémons");

    return await response.json();
}