import "./Home.css";
import Header from "../../components/Headers/Header";
import PokemonHome from "../../components/PokemonHome/PokemonHome";
import { useState, useEffect } from "react";
import { getPokemons } from "../../services/pokemonService";

function Home() {

    const [pokemons, setPokemons] = useState([]);
    const [pagination, setPagination] = useState(null);

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    
    const [text, setText] = useState('');
    const [generation, setGeneration] = useState('');
    const [type, setType] = useState('');
    const [region, setRegion] = useState('');

    const [sort, setSort] = useState('pokedex-asc');
    const [page, setPage] = useState(1);


    const handleChange = (event) => {
        setText(event.target.value);
    };

    const handleGenerationChange = (event) => {
        setGeneration(event.target.value);
    };

    const handleTypeChange = (event) => {
        setType(event.target.value);
    };

    const handleRegionChange = (event) => {
        setRegion(event.target.value);
    }

    const handleSortChange = (event) => {
        setSort(event.target.value);
    };

    const handleSearch = () => {
        setPage(1);
        carregarPokemons(1, text, generation, type, region, sort);
    };

    const handlePokemonClick = (pokemon) => {
        // Redireciona para a página de detalhes do Pokémon
        window.location.href = `/pokemon/${pokemon.pokedexNumber}`;
    }

    async function carregarPokemons(currentPage = page, search = "", generation = "", type = "", region = "", sort = "pokedex-asc") {

        setLoading(true);
        setError(null);

        try {
            const result = await getPokemons(currentPage, 50, search, generation, type, region, sort);
            setPokemons(result.data);
            setPagination(result.pagination);
        }
        catch (error) {
            console.error(error);
            setError("Não foi possível carregar os pokémons.");
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        carregarPokemons(page, text, generation, type, region, sort);
    }, []);

    return (
        <>
            <Header />

            <main className="home">

                <section className="search-section">

                    <input type="text" value={text} onChange={handleChange} placeholder="Pesquisar Pokémon..."/>

                    <button onClick={handleSearch}>Buscar</button>

                    <select value={sort} onChange={handleSortChange}>
                        <option value="pokedex-asc">Numeração Pokédex [0-9]</option>
                        <option value="pokedex-desc">Numeração Pokédex [9-0]</option>
                        <option value="name-asc">Nome [A-Z]</option>
                        <option value="name-desc">Nome [Z-A]</option>
                    </select>

                </section>

                <section className="filters">

                    <select value={type} onChange={handleTypeChange}>
                        <option value="">Tipo</option>
                        <option value="Normal">Normal</option>
                        <option value="Fire">Fire</option>
                        <option value="Water">Water</option>
                        <option value="Grass">Grass</option>
                        <option value="Flying">Flying</option>
                        <option value="Fighting">Fighting</option>
                        <option value="Poison">Poison</option>
                        <option value="Electric">Electric</option>
                        <option value="Ground">Ground</option>
                        <option value="Rock">Rock</option>
                        <option value="Psychic">Psychic</option>
                        <option value="Ice">Ice</option>
                        <option value="Bug">Bug</option>
                        <option value="Ghost">Ghost</option>
                        <option value="Steel">Steel</option>
                        <option value="Dragon">Dragon</option>
                        <option value="Dark">Dark</option>
                        <option value="Fairy">Fairy</option>
                    </select>

                    <select value={generation} onChange={handleGenerationChange}>
                        <option value="">Geração</option>
                        <option value="Gen1">Geração 1</option>
                        <option value="Gen2">Geração 2</option>
                        <option value="Gen3">Geração 3</option>
                        <option value="Gen4">Geração 4</option>
                        <option value="Gen5">Geração 5</option>
                        <option value="Gen6">Geração 6</option>
                        <option value="Gen7">Geração 7</option>
                        <option value="Gen8">Geração 8</option>
                        <option value="Gen9">Geração 9</option>
                    </select>

                    <select value={region} onChange={handleRegionChange}>
                        <option value="">Região</option>
                        <option value="Kanto">Kanto</option>
                        <option value="Johto">Johto</option>
                        <option value="Hoenn">Hoenn</option>
                        <option value="Sinnoh">Sinnoh</option>
                        <option value="Unova">Unova</option>
                        <option value="Kalos">Kalos</option>
                        <option value="Alola">Alola</option>
                        <option value="Galar">Galar</option>
                        <option value="Paldea">Paldea</option>
                    </select>

                </section>

                <section className="pokemon-grid">
                
                {/* Cria mensagem de loading caso os pokémons estejam sendo carregados */}
                {loading && <div className="loading"><div className="spinner"></div>Carregando pokémons...</div> }

                {/* Cria mensagem de erro caso ocorra algum problema ao carregar os pokémons */}
                {!loading && error && ( <div className="loading">{error}</div> )}

                {/* Cria mensagem caso não haja pokémons encontrados */}
                {!loading && !error && pokemons.length === 0 && ( <div className="loading">Nenhum Pokémon encontrado.</div> )}

                {/* Renderiza os pokémons caso não haja loading, erro e haja pokémons */}
                {!loading && !error && pokemons.length > 0 && pokemons.map(pokemon => ( 
                    <PokemonHome key={pokemon.pokedexNumber} pokemon={pokemon} onClick={() => handlePokemonClick(pokemon)} /> ))
                }

                </section>

                <section className="pagination">
                    <div className= "pagination-buttons">
                        <button onClick={() => {
                            setPage(1);
                            carregarPokemons(1, text, generation, type, region, sort); }}
                            disabled={page === 1}
                            aria-label="Primeira página" 
                            title="Primeira página">
                                <svg class="button-icon" 
                                    fill="none" 
                                    stroke="currentColor" 
                                    viewBox="0 0 24 24">
                                    <path 
                                        stroke-linecap="round" 
                                        stroke-linejoin="round" 
                                        stroke-width="2" 
                                        d="M18 19l-7-7 7-7">
                                    </path>
                                    <path 
                                        stroke-linecap="round" 
                                        stroke-linejoin="round" 
                                        stroke-width="2" 
                                        d="M11 19l-7-7 7-7">
                                    </path>
                                </svg>
                        </button>

                        <button onClick={() => { 
                                setPage(page - 1); 
                                carregarPokemons(page - 1, text, generation, type, region, sort); }} 
                                disabled={page === 1}
                                aria-label="Página anterior"
                                title="Página anterior">
                                    <svg class="button-icon" 
                                        fill="none" 
                                        stroke="currentColor"
                                        viewBox="0 0 24 24">
                                        <path 
                                            stroke-linecap="round" 
                                            stroke-linejoin="round" 
                                            stroke-width="2" 
                                            d="M15 19l-7-7 7-7">
                                        </path>
                                    </svg>
                        </button>

                        <button onClick={() => { 
                                setPage(page + 1); 
                                carregarPokemons(page + 1, text, generation, type, region, sort); }} 
                                disabled={pagination && !pagination.hasNext}
                                aria-label="Próxima página" 
                                title="Próxima página">
                                    <svg class="button-icon" 
                                        fill="none" 
                                        stroke="currentColor" 
                                        viewBox="0 0 24 24">
                                        <path 
                                            stroke-linecap="round" 
                                            stroke-linejoin="round" 
                                            stroke-width="2" 
                                            d="M9 5l7 7-7 7">
                                        </path>
                                    </svg>
                        </button>

                        <button onClick={() => {
                                setPage(pagination.totalPages); 
                                carregarPokemons(pagination.totalPages, text, generation, type, region, sort); }}
                                disabled={!pagination || page === pagination.totalPages}
                                aria-label="Última página"
                                title="Última página">
                                    <svg class="button-icon" 
                                        fill="none" 
                                        stroke="currentColor" 
                                        viewBox="0 0 24 24">
                                        <path 
                                            stroke-linecap="round" 
                                            stroke-linejoin="round" 
                                            stroke-width="2" 
                                            d="M6 5l7 7-7 7">
                                        </path>
                                        <path 
                                            stroke-linecap="round" 
                                            stroke-linejoin="round" 
                                            stroke-width="2" 
                                            d="M13 5l7 7-7 7">
                                        </path>
                                    </svg>
                        </button>
                    </div>
                    
                    <p>Página {page} de {pagination ? pagination.totalPages : 1}</p>
                    
                </section>

            </main>
        </>
    );
}

export default Home;