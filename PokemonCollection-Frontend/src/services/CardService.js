const API_URL = "https://localhost:7238/api/CollectionEntry";

const API_CARDS_URL = "https://localhost:7238/api/Card"

export async function getCardByPokedexNumber(pokedexNumber) {
    const response = await fetch(`${API_URL}/${pokedexNumber}`);
    if (!response.ok) throw new Error("Erro ao buscar a carta.");
    return await response.json();
}

export async function getCardsByPokedexNumber(pokedexNumber, page = 1, pageSize = 25) {
    let url = `${API_CARDS_URL}/${pokedexNumber}/cards`;
    url += `?PageNumber=${page}&PageSize=${pageSize}`;
    
    const response = await fetch(url)
    if (!response.ok) throw new Error("Erro ao buscar as cartas.");

    const cards = await response.json();
    const pagination = JSON.parse(response.headers.get("X-Pagination"));

    return { 
        data: cards, 
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