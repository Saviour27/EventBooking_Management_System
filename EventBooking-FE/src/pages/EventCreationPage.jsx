// EventCreationPage.jsx

import React, { useState } from 'react';
import axios from 'axios';

const EventCreationPage = () => {
  // State variables to store form data
  const [name, setName] = useState('');
  const [startTime, setStartTime] = useState('');
  const [endTime, setEndTime] = useState('');
  const [location, setLocation] = useState('');
  const [ticketPrice, setTicketPrice] = useState('');
  const [totalTickets, setTotalTickets] = useState('');

  // Function to handle form submission
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      // Make an HTTP POST request to the backend endpoint for creating events
      await axios.post('/api/events', {
        name,
        startTime,
        endTime,
        location,
        ticketPrice,
        totalTickets
      });
      // Reset form fields after successful submission
      setName('');
      setStartTime('');
      setEndTime('');
      setLocation('');
      setTicketPrice('');
      setTotalTickets('');
      alert('Event created successfully!');
    } catch (error) {
      console.error('Error creating event:', error);
      alert('Failed to create event. Please try again.');
    }
  };

  return (
    <div>
      <h1>Create New Event</h1>
      <form onSubmit={handleSubmit}>
        <label>
          Name:
          <input type="text" value={name} onChange={(e) => setName(e.target.value)} />
        </label>
        <br />
        <label>
          Start Time:
          <input type="datetime-local" value={startTime} onChange={(e) => setStartTime(e.target.value)} />
        </label>
        <br />
        <label>
          End Time:
          <input type="datetime-local" value={endTime} onChange={(e) => setEndTime(e.target.value)} />
        </label>
        <br />
        <label>
          Location:
          <input type="text" value={location} onChange={(e) => setLocation(e.target.value)} />
        </label>
        <br />
        <label>
          Ticket Price:
          <input type="number" value={ticketPrice} onChange={(e) => setTicketPrice(e.target.value)} />
        </label>
        <br />
        <label>
          Total Tickets:
          <input type="number" value={totalTickets} onChange={(e) => setTotalTickets(e.target.value)} />
        </label>
        <br />
        <button type="submit">Create Event</button>
      </form>
    </div>
  );
};

export default EventCreationPage;
