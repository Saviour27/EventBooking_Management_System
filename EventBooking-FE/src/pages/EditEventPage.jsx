import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import axios from 'axios';

const EditEventPage = () => {
    const { eventId } = useParams(); // Get eventId from URL params
    const [event, setEvent] = useState(null);
    const [formData, setFormData] = useState({
        Name: '',
        StartTime: '',
        EndTime: '',
        Location: '',
        TicketPrice: '',
        TotalTickets: ''
    });

    // Fetch event details from the backend when the component mounts
    useEffect(() => {
        // Fetch event details from the backend API and update the event state
        const fetchEvent = async () => {
            try {
                // Make API call to fetch event details using Axios
                const response = await axios.get(`api/events/${eventId}`);
                setEvent(response.data);
                // Set initial form data with event details
                setFormData({
                    Name: response.data.Name,
                    StartTime: response.data.StartTime,
                    EndTime: response.data.EndTime,
                    Location: response.data.Location,
                    TicketPrice: response.data.TicketPrice,
                    TotalTickets: response.data.TotalTickets
                });
            } catch (error) {
                console.error('Error fetching event details:', error);
            }
        };
        fetchEvent();
    }, [eventId]); // Add eventId to dependency array to re-fetch event when it changes

    // Function to handle form submission (updating event details)
    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            // Make API call to update event details using Axios
            await axios.put(`api/events/${eventId}`, formData);
            // Redirect to AdminDashboard or show success message
            // You can implement this based on your application's navigation flow
            // Example: window.location.href = '/admin-dashboard';
        } catch (error) {
            console.error('Error updating event:', error);
        }
    };

    // Function to handle input changes
    const handleInputChange = (event) => {
        setFormData({ ...formData, [event.target.name]: event.target.value });
    };

    if (!event) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <h2>Edit Event</h2>
            {/* Render a form to edit event details */}
            <form onSubmit={handleSubmit}>
                <label>
                    Name:
                    <input type="text" name="Name" value={formData.Name} onChange={handleInputChange} />
                </label>
                <label>
                    StartTime:
                    <input type="datetime-local" name="StartTime" value={formData.StartTime} onChange={handleInputChange} />
                </label>
                <label>
                    EndTime:
                    <input type="datetime-local" name="EndTime" value={formData.EndTime} onChange={handleInputChange} />
                </label>
                <label>
                    Location:
                    <input type="text" name="Location" value={formData.Location} onChange={handleInputChange} />
                </label>
                <label>
                    TicketPrice:
                    <input type="number" name="TicketPrice" value={formData.TicketPrice} onChange={handleInputChange} />
                </label>
                <label>
                    TotalTickets:
                    <input type="number" name="TotalTickets" value={formData.TotalTickets} onChange={handleInputChange} />
                </label>
                <button type="submit">Save Changes</button>
                {/* Link to go back to AdminDashboard */}
                <Link to="/admin-dashboard">Cancel</Link>
            </form>
        </div>
    );
};

export default EditEventPage;
