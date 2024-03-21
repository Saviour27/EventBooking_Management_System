import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

const AdminDashboard = () => {
    const [events, setEvents] = useState([]);

    // Fetch events from the backend when the component mounts
    useEffect(() => {
        // Fetch events from the backend API and update the events state
        const fetchEvents = async () => {
            try {
                // Make API call to fetch events using Axios
                const response = await axios.get('api/events');
                setEvents(response.data);
            } catch (error) {
                console.error('Error fetching events:', error);
            }
        };
        fetchEvents();
    }, []);

    // Function to handle event deletion
    const deleteEvent = async (eventId) => {
        try {
            // Make API call to delete event using Axios
            await axios.delete(`api/events/${eventId}`);
            // Update events state after deletion
            setEvents(events.filter(event => event.id !== eventId));
        } catch (error) {
            console.error('Error deleting event:', error);
        }
    };

    return (
        <div>
            <h2>Admin Dashboard</h2>
            <Link to="/create-event">
                <button>Create New Event</button>
            </Link>
            <ul>
                {events.map(event => (
                    <li key={event.id}>
                        <div style={{ display: 'flex', alignItems: 'center' }}>
                            <div>
                                <h3>{event.name}</h3>
                                <p>Date: {event.date}</p>
                            </div>
                            {/* Edit Event Button */}
                            <Link to={`/edit-event/${event.id}`}>
                                <button>Edit</button>
                            </Link>
                            {/* Delete Event Button */}
                            <button onClick={() => deleteEvent(event.id)}>Delete</button>
                            {/* List of Users Button */}
                            <Link to={`/event-users/${event.id}`}>
                                <button>List of Users</button>
                            </Link>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default AdminDashboard;
