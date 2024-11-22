import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => {
    return (
        <nav>
            <ul>
                <li><Link to="/">Home</Link></li>
                <li><Link to="/hospitals">Hospitals</Link></li>
                <li><Link to="/hospital-search">Search Hospital</Link></li>
                <li><Link to="/rosters">Rosters</Link></li>
                <li><Link to="/roster-search">Search Roster</Link></li>
                <li><Link to="/wards">Wards</Link></li>
                <li><Link to="/ward-search">Search Ward</Link></li>
            </ul>
        </nav>
    );
};

export default Header;
