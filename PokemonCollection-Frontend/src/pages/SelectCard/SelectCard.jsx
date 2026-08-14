import "./SelectCard.css";

import Header from "../../components/Headers/Header";
import CardGrid from "../../components/CardGrid/CardGrid";
import { getCardsByPokedexNumber } from "../../services/CardService";

import { Link } from "react-router-dom";
import { useParams } from "react-router-dom";

import {useState, useEffect} from "react";


function SelectCard() {
    const { pokedexNumber } = useParams()

    const [cards, setCards] = useState([])
    const [pagination, setPagination] = useState(null);

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const [page, setPage] = useState(1);

    async function carregarCards(pageAtual = page) {
        setLoading(true);
        setError(null);

        try {
            const result = await getCardsByPokedexNumber(pokedexNumber, pageAtual)
            setCards(result.data);
            setPagination(result.pagination);
        }
        catch (error) {
            console.error(error);
            setError("Não foi possível carregar as cartas.");
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        carregarCards(page);
    }, [page]);

    return (
        <>    
            <Header />
            <div className="select-card-page">
                <Link to={`/pokemon/${pokedexNumber}`}>
                    <button className="btn-back">Voltar</button>
                </Link>
                
                <div className="select-card-body">

                    <div className="search-box">
                        <h1 className="search-box-h1">Filtros</h1>
                        <p className="search-box-p">Nome da Carta</p>
                        <input className="search-box-input" type="text" placeholder="Pesquisar Carta..."/>

                        <p className="search-box-p">Numero da Carta</p>
                        <input className="search-box-input" type="text" placeholder="Ex: 79/217, 14/86..."/>

                        <p className="search-box-p">Nome da Coleção</p>
                        <input className="search-box-input" type="text" placeholder="Ex: Ascended Heroes, Pitch Black..."/>

                        <p className="search-box-p">Raridade</p>
                        <select className="search-box-select">
                            <option value="">Todas</option>
                        </select>

                        <p className="search-box-p">Lingua</p>
                        <select className="search-box-select">
                            <option value="">Todas</option>
                        </select>

                        <p className="search-box-p">Extra</p>  
                        <select className="search-box-select">
                            <option value="">Nenhum</option>
                        </select>

                        <div className="search-box-btn">
                            <button className="btn-search">Buscar</button>
                            <button className="btn-clear">Limpar</button>  
                        </div>
                    
                    </div>

                    <div className="select-card-main">
                        <section className="select-card-grid">
                            {loading && <div className="loading"><div className="spinner"></div>Carregando cartas...</div> }
                            {!loading && error && ( <div className="loading">{error}</div> )}
                            {!loading && !error && cards.length === 0 && ( <div className="loading">Nenhuma carta encontrada.</div> )}

                            {!loading && !error && cards.length > 0 && cards.map(card => ( 
                                <CardGrid key={card.id} card={card} /> ))
                            }
                        </section>
                        <section className="pagination">
                            <div className= "pagination-buttons">
                                <button onClick={() => {
                                    setPage(1);
                                    carregarCards(1); }}
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
                                            carregarCartas(page - 1); }} 
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
                                            carregarPokemons(page + 1); }} 
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
                                            carregarPokemons(pagination.totalPages); }}
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
                    </div>
                </div>
                
            </div>
            
        </>
    );
}

export default SelectCard;