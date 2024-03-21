import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

const UserDashboard = () => {
    const [events, setEvents] = useState([]);
    const [bookedEvents, setBookedEvents] = useState([]);
    const [walletBalance, setWalletBalance] = useState(0);

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

        // Fetch booked events from the backend API
        const fetchBookedEvents = async () => {
            try {
                // Make API call to fetch booked events using Axios
                const response = await axios.get('api/booked-events');
                setBookedEvents(response.data);
            } catch (error) {
                console.error('Error fetching booked events:', error);
            }
        };

        // Fetch wallet balance from the backend API
        const fetchWalletBalance = async () => {
            try {
                // Make API call to fetch wallet balance using Axios
                const response = await axios.get('api/wallet/balance');
                setWalletBalance(response.data.balance);
            } catch (error) {
                console.error('Error fetching wallet balance:', error);
            }
        };

        fetchEvents();
        fetchBookedEvents();
        fetchWalletBalance();
    }, []);

    // Function to handle event booking
    const bookEvent = async (eventId) => {
        try {
            // Make API call to book event using Axios
            await axios.post(`api/events/${eventId}/book`);
            // Refetch booked events after booking
            fetchBookedEvents();
        } catch (error) {
            console.error('Error booking event:', error);
        }
    };

    // Function to handle cancelling booked event
    const cancelBooking = async (eventId) => {
        try {
            // Make API call to cancel booking using Axios
            await axios.post(`api/booked-events/${eventId}/cancel`);
            // Refetch booked events after cancellation
            fetchBookedEvents();
        } catch (error) {
            console.error('Error cancelling booking:', error);
        }
    };

    // Function to handle topping up wallet balance
    const topUpWallet = () => {
        // Redirect to the top-up page
        // Implement the top-up page using React Router
    };

    return (
        <div>
            <h2>User Dashboard</h2>
            <h3>Events</h3>
            <ul>
                {events.map(event => (
                    <li key={event.id}>
                        <div>
                            <h4>{event.name}</h4>
                            <p>Date: {event.date}</p>
                            <button onClick={() => bookEvent(event.id)}>Book</button>
                        </div>
                    </li>
                ))}
            </ul>

            <h3>Booked Events</h3>
            <ul>
                {bookedEvents.map(event => (
                    <li key={event.id}>
                        <div>
                            <h4>{event.name}</h4>
                            <p>Date: {event.date}</p>
                            <button onClick={() => cancelBooking(event.id)}>Cancel Booking</button>
                        </div>
                    </li>
                ))}
            </ul>

            <h3>Wallet Balance</h3>
            <p>Balance: ${walletBalance}</p>
            <button onClick={topUpWallet}>Top Up</button>
        </div>
    );
};

export default UserDashboard;
