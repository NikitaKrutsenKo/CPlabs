import axios from 'axios';
import React, { useEffect, useState } from 'react';

const RosterList = () => {
    const [rosters, setRosters] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchRosters = async () => {
            try {
                const response = await axios.get('http://localhost:5000/api/roster');
                setRosters(response.data);
            } catch (error) {
                setError('There was an error fetching the rosters!');
            }
        };

        fetchRosters();
    }, []);

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h1>Rosters</h1>
            <ul>
                {rosters.map(roster => (
                    <li key={roster.roster_ID}>
                        {roster.staffName} - {roster.shiftName} ({roster.startDate} - {roster.endDate})
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default RosterList;
