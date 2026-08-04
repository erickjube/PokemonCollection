import "./Home.css";
import Header from "../../components/Headers/Header";
import PokemonCard from "../../components/PokemonCards/PokemonCard";
import { useState, useEffect } from "react";
import { getPokemons } from "../../services/pokemonService";

function Home() {

    const [pokemons, setPokemons] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [text, setText] = useState('');
    const [generation, setGeneration] = useState('');
    const [type, setType] = useState('');
    const [region, setRegion] = useState('');
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

    const handleBuscar = () => {
        setPage(1);
        carregarPokemons(1, text, generation, type, region);
    };

    async function carregarPokemons(pageAtual = page, pesquisa = "", geracao = "", tipo = "", regiao = "") {

        setLoading(true);
        setError(null);

        try {
            const data = await getPokemons(pageAtual, 50, pesquisa, geracao, tipo, regiao);
            setPokemons(data);
        }
        catch {
            setError("Não foi possível carregar os pokémons.");
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        carregarPokemons(page, text, generation, type, region);
    }, []);

    return (
        <>
            <Header />

            <main className="home">

                <section className="search-section">

                    <input type="text" value={text} onChange={handleChange} placeholder="Pesquisar Pokémon..."/>

                    <button onClick={handleBuscar}>Buscar</button>

                    <select>
                        <option>Numeração Pokedex [0-9]</option>
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
                {!loading && !error && pokemons.length > 0 && pokemons.map(pokemon => ( <PokemonCard key={pokemon.id} pokemon={pokemon} /> ))}

                </section>

            </main>
        </>
    );
}

export default Home;