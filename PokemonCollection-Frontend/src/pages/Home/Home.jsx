import "./Home.css";
import Header from "../../components/Headers/Header";
import PokemonCard from "../../components/PokemonCards/PokemonCard";
import { useState, useEffect } from "react";
import { getPokemons } from "../../services/pokemonService";

function Home() {

    const [pokemons, setPokemons] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function carregarPokemons() {
            try {
                const data = await getPokemons();
                setPokemons(data);
            }
            catch (erro) {
                setError("Não foi possível carregar os pokémons.");
            }
            finally {
                setLoading(false);
            }
        }

        carregarPokemons();

    }, []);


    return (
        <>
            <Header />

            <main className="home">

                <section className="search-section">

                    <input type="text" placeholder="Pesquisar Pokémon..."/>

                    <button>Buscar</button>

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
                
                {loading && 
                    <div className="loading">
                        <div className="spinner"></div> 
                        Carregando pokémons...
                    </div>
                }

                {!loading && error && ( <div className="loading">{error}</div> )}

                {!loading && !error &&
                    pokemons.map(pokemon => ( <PokemonCard key={pokemon.id} pokemon={pokemon} /> ))
                }
                </section>

            </main>
        </>
    );
}

export default Home;