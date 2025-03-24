# Game Night Scores Right 🎲🏆

Welcome to **Game Night Scores Right** – your ultimate tool for keeping score, tracking players, teams, and events during those epic game nights! Whether you're a board game fanatic, a trivia master, or a competitive team sport enthusiast, this API has got your back (and your leaderboard)!

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Setup & Installation](#setup--installation)
- [Running Database Migrations with DbUp](#running-database-migrations-with-dbup)
- [Running the Tests](#running-the-tests)
- [License](#license)

---

## Overview

**Game Night Scores Right** is a full-stack project with a robust **.NET 8 API** backend that keeps track of accounts, users, players, teams, and game events. It's built using **ASP.NET Core**, **Entity Framework Core**, and is designed with scalability and fun in mind.

---

## Features

- **Accounts & Users:**  
  Manage account creation, linking users, and handling authentication-related tasks.

- **Game Events:**  
  Schedule and manage events for your game nights.

- **Teams & Players:**  
  Track team scores and individual player stats with ease.

- **Test Driven:**  
  A solid suite of tests keeps the code reliable as we roll out new features.

---

## Development Status

Please note that **Game Night Scores Right** is still under active development. As I continue working on the project, you can expect:

- **Expanded API Functionality:**  
  More robust and diverse endpoints will be added, improving the overall capability of the API.

- **Frontend Integration:**  
  A user-friendly frontend is planned for future development, which will complement the API and enhance the overall user experience.

Thank you for checking out the project in its early form. Stay tuned for updates as the project evolves!

---

## Tech Stack

- **Backend:** .NET 8, ASP.NET Core Web API
- **Data Access:** Entity Framework Core (with SQL Server)
- **Testing:** MSTest & Fluent Assertions

---

## Setup & Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/skylerreising/game-night-scores-right.git
   cd game-night-scores-right
   ```

2. **Configure your environment:**

   - Ensure you have .NET 8 SDK installed.
   - Set up your connection strings and other sensitive settings. You can use environment variables or maintain a template file (e.g., appsettings.example.json) that you copy to appsettings.json locally. Note: Make sure appsettings.json is listed in your .gitignore.

3. **Install dependencies:**

   ```bash
   dotnet restore
   dotnet build
   ```

4. **Run the API:**
   Navigate to your backend project folder (for example, backend/GameNightScoresRight.API) and run:

   ```bash
    dotnet run
   ```

5. **Explore the API with Swagger:**
   Open your browser and navigate to `http://localhost:5000/swagger` to see the API documentation and try out the endpoints.
   (Or use the URL/port defined in your launchSettings.json.) This lets you explore and test the API endpoints interactively.

---

## Running Database Migrations with DbUp

To update the database schema using DbUp:

1. Create a Local Configuration File for DbUp:

   In the DbUpRunner directory (located at backend/DbUpRunner), create an appsettings.json file with your connection strings:

   ```json
   {
     "ConnectionStrings": {
       "GameNightDB": "<YOUR_CONNECTION_STRING>",
       "GameNightTestDB": "<YOUR_TEST_CONNECTION_STRING>"
     }
   }
   ```

2. Place Your SQL Scripts:

   Ensure your SQL scripts (e.g., 001-initial-tables.sql) are located in a folder (for example, SQL Scripts) inside the SQL Scripts directory.

3. Run the DbUpRunner:

   Open a terminal (GitBash or Developer PowerShell), navigate to the DbUpRunner directory, and run:

   ```bash
   dotnet run
   ```

   The DbUpRunner will read the connection strings from its local appsettings.json file and apply the SQL scripts to both the production and test databases. It logs its progress and reports success or any errors encountered.

## Running the Tests

This project uses MSTest along with Fluent Assertions to ensure everything works as expected.
To run the tests, simply execute:

```bash
dotnet test
```

The tests are located in the backend/GameNightTests directory and will verify that your accessors, managers, and other components perform correctly.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

Happy Gaming!
