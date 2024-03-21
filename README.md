This repository contains the source code for the Event Booking backend API and React frontend for consuming the API. The API is built using ASP.NET Core and provides endpoints for managing events, bookings, and wallet transactions.

Getting Started Follow these steps to run the backend API on your local machine.

Prerequisites 
.NET SDK installed 
MySQL database server installed and running

Installation 
Clone the repository:

Navigate to the project directory:

bash
Copy code 
cd event-booking-api

Database Setup 
Create a MySQL database named myDatabase. 
Update the database connection string in the appsettings.json file: json Copy code "ConnectionStrings": { "DefaultConnection": "Server=localhost;Port=3307;Database=myDatabase;Uid=root;Pwd=Saviola@27;" }

Running the API Restore dependencies and build the project:

bash 
Copy code 
dotnet restore dotnet build

Apply EF Core migrations to create the database schema:

bash 
Copy code 
dotnet ef database update

Run the API:

bash 
Copy code 
dotnet run 
The API should now be running on https://localhost:5001.

API Endpoints 
/api/events: Get all events, 
create a new event 
/api/events/{id}: Get event by ID, update event, delete event 
/api/events/{id}/registrations: Get user registrations for an event 
/api/bookings: Get all bookings, create a new booking 
/api/bookings/{id}: Get booking by ID, cancel booking 
/api/users: Get all users, create a new user 
/api/users/{id}: Get user by ID, update user, delete user 

For detailed documentation of API endpoints, refer to the API documentation.

Additional Resources 
ASP.NET Core Documentation 
Entity Framework Core Documentation 
MySQL Documentation
