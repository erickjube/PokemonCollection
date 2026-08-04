const API_URL = "https://localhost:7238/api/Pokemon";

export async function getPokemons(page = 1, pageSize = 50, search = "", generation = "", type = "", region = "") {
    
    let url = `${API_URL}?PageNumber=${page}&PageSize=${pageSize}`;

    if (search) url += `&Search=${encodeURIComponent(search)}`;
    
    if (generation) url += `&Generation=${encodeURIComponent(generation)}`;

    if (type) url += `&Type=${encodeURIComponent(type)}`;

    if (region) url += `&Region=${encodeURIComponent(region)}`;

    const response = await fetch(url);

    if (!response.ok) throw new Error("Erro ao buscar os pokémons");

    return response.json();
}