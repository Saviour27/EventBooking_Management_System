import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"; // Make sure to import BrowserRouter and Routes
import LandingPage from "./pages/LandingPage";
import AdminDashboard from "./pages/AdminDashboard";
import UserDashboard from "./pages/UserDashboard";
import EventCreationPage from "./pages/EventCreationPage";
import TopUpPage from "./pages/TopUpPage";
//import EditEventPage from "./pages/EditEventPage";
//import UserListPage from "./pages/UserListPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/admin" element={<AdminDashboard />} />
        <Route path="/user" element={<UserDashboard />} />
        <Route path="/create-event" element={<EventCreationPage />} />
        <Route path="/top-up" element={<TopUpPage />} />
        
      </Routes>
    </Router>
  );
}

export default App;
