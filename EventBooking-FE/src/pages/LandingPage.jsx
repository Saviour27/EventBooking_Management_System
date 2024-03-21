import React from 'react';

import { Link } from 'react-router-dom';

const LandingPage = () => {
  return (
    <div>
     
      <h1>Welcome to my Booking System</h1>
      <p>Please select a dashboard:</p>
      <ul>
        <li><Link to="/user">User Dashboard</Link></li>
        <li><Link to="/admin">Admin Dashboard</Link></li>
      </ul>
    </div>
  );
};

export default LandingPage;