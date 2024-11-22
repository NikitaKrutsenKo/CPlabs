import axios from 'axios';
import React, { useEffect, useState } from 'react';

const WardList = () => {
    const [wards, setWards] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchWards = async () => {
            try {
                const response = await axios.get('http://localhost:7106/api/Ward');
                setWards(response.data);
            } catch (error) {
                setError('There was an error fetching the wards!');
            }
        };

        fetchWards();
    }, []);

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h1>Wards</h1>
            <ul>
                {wards.map(ward => (
                    <li key={ward.ward_ID}>
                        {ward.wardName} - {ward.wardLocation}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default WardList;
