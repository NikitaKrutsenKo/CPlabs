import axios from 'axios';
import React, { useState } from 'react';

const WardSearch = () => {
    const [wardId, setWardId] = useState('');
    const [ward, setWard] = useState(null);
    const [error, setError] = useState(null);

    const handleSearch = async () => {
        try {
            const response = await axios.get(`http://localhost:5000/api/ward/${wardId}`);
            setWard(response.data);
            setError(null);
        } catch (error) {
            setError('Ward not found!');
            setWard(null);
        }
    };

    return (
        <div>
            <h1>Search Ward</h1>
            <input
                type="text"
                value={wardId}
                onChange={(e) => setWardId(e.target.value)}
                placeholder="Enter Ward ID"
            />
            <button onClick={handleSearch}>Search</button>
            {ward && (
                <div>
                    <h2>{ward.wardName}</h2>
                    <p>{ward.wardLocation}</p>
                    <p>{ward.wardDescription}</p>
                </div>
            )}
            {error && <div>{error}</div>}
        </div>
    );
};

export default WardSearch;
