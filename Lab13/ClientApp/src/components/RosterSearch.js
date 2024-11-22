import axios from 'axios';
import React, { useState } from 'react';

const RosterSearch = () => {
    const [rosterId, setRosterId] = useState('');
    const [roster, setRoster] = useState(null);
    const [error, setError] = useState(null);

    const handleSearch = async () => {
        try {
            const response = await axios.get(`http://localhost:5000/api/roster/${rosterId}`);
            setRoster(response.data);
            setError(null);
        } catch (error) {
            setError('Roster not found!');
            setRoster(null);
        }
    };

    return (
        <div>
            <h1>Search Roster</h1>
            <input
                type="text"
                value={rosterId}
                onChange={(e) => setRosterId(e.target.value)}
                placeholder="Enter Roster ID"
            />
            <button onClick={handleSearch}>Search</button>
            {roster && (
                <div>
                    <h2>{roster.staffName}</h2>
                    <p>{roster.shiftName}</p>
                    <p>{roster.startDate} - {roster.endDate}</p>
                </div>
            )}
            {error && <div>{error}</div>}
        </div>
    );
};

export default RosterSearch;
