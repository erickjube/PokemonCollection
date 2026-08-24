import { Link } from "react-router-dom";
import "./Header.css";

function Header() {
    return (
        <header className="header">

            <div className="logo">
                <Link to="/">Pokédex</Link>
            </div>

            <nav className="menu">
                <Link to="/collection">Login</Link>
            </nav>

        </header>
    );
}

export default Header;