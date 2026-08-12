const API_URL = "https://localhost:7238/api/Pokemon";


export async function getPokemons(page = 1, pageSize = 50, search = "", generation = "", type = "", region = "", sort = "pokedex-asc") {
    let sortBy = "PokedexNumber";
    let descending = false;
    let url = `${API_URL}?PageNumber=${page}&PageSize=${pageSize}`;
    
    switch (sort) {
        case "pokedex-asc":
            sortBy = "PokedexNumber";
            descending = false;
            break;

        case "pokedex-desc":
            sortBy = "PokedexNumber";
            descending = true;
            break;

        case "name-asc":
            sortBy = "Name";
            descending = false;
            break;

        case "name-desc":
            sortBy = "Name";
            descending = true;
            break;
    }

    if (search) url += `&Search=${encodeURIComponent(search)}`;
    if (generation) url += `&Generation=${encodeURIComponent(generation)}`;
    if (type) url += `&Type=${encodeURIComponent(type)}`;
    if (region) url += `&Region=${encodeURIComponent(region)}`;
    url += `&SortBy=${encodeURIComponent(sortBy)}`;
    url += `&Descending=${descending}`;

    const response = await fetch(url);
    if (!response.ok) throw new Error("Erro ao buscar os pokémons");

    const pokemons = await response.json();
    const pagination = JSON.parse(response.headers.get("X-Pagination"));

    return { 
        data: pokemons, 
        pagination: {
            currentPage: pagination.CurrentPage,
            totalPages: pagination.TotalPages,
            totalCount: pagination.TotalCount,
            pageSize: pagination.PageSize,
            hasNext: pagination.HasNext,
            hasPrevious: pagination.HasPrevious
        }
    };
}

export async function getPokemonByPokedexNumber(pokedexNumber) {
    const response = await fetch(`${API_URL}/pokedex/${pokedexNumber}`);
    if (!response.ok) throw new Error("Erro ao buscar o pokémon");
    return await response.json();
}
