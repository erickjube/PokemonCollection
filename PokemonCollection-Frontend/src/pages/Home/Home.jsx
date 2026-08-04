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
    const [page, setPage] = useState(1);


    const handleChange = (event) => {
        setText(event.target.value);
    };

    async function carregarPokemons(pageAtual = page, pesquisa = "") {

        setLoading(true);
        setError(null);

        try {
            const data = await getPokemons(pageAtual, 50, pesquisa);
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
        carregarPokemons(page + 1, text);
    }, []);

    return (
        <>
            <Header />

            <main className="home">

                <section className="search-section">

                    <input type="text" value={text} onChange={handleChange} placeholder="Pesquisar Pokémon..."/>

                    <button onClick={() => { setPage(1); carregarPokemons(1, text); }}>Buscar</button>

                    <select>
                        <option>Numeração Pokedex [0-9]</option>
                    </select>

                </section>

                <section className="filters">

                    <select>
                        <option>Tipo</option>
                    </select>

                    <select>
                        <option>Geração</option>
                    </select>

                    <select>
                        <option>Região</option>
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