import "./App.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";

import Home from "./pages/Home/Home";
import PokemonDetails from "./pages/PokemonDetails/PokemonDetails";
import SelectCard from "./pages/SelectCard/SelectCard"

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/pokemon/:pokedexNumber" element={<PokemonDetails />} />
                <Route path="/cards/:pokedexNumber" element={<SelectCard />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;