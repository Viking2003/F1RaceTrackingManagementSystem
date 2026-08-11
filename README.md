
# F1RaceTrackingManagementSystem
An .NET Core 8.0 based MVC project for management of tracks, races, drivers regarding Formula 1.
=======
# 🏎️ F1 Race Tracker — .NET Core MVC

A full-featured Formula 1 race tracking management system built with ASP.NET Core 8 MVC and Entity Framework Core.

## Features

- **Dashboard** — Season standings, upcoming races, recent results at a glance
- **Race Management** — Full CRUD for races with season filtering, status tracking
- **Race Results** — Add/view detailed results with positions, lap times, pit stops, fastest laps
- **Driver Profiles** — Career stats, race history, championship records
- **Team Management** — Constructor profiles with driver rosters and color coding
- **Auto-calculated Points** — Points (25/18/15/12/10/8/6/4/2/1 + fastest lap) computed on save
- **Seeded Data** — Real 2024/2025 F1 teams, drivers, and race calendar pre-loaded

## Tech Stack

| Component | Technology |
|---|---|
| Framework | ASP.NET Core 8 MVC |
| ORM | Entity Framework Core 8 |
| Database | SQLite (auto-created on startup) |
| UI | Bootstrap 5, Font Awesome |
| Language | C# 12 |

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Run the App

```bash
cd F1RaceTracker
dotnet restore
dotnet run
```

Then open: `https://localhost:5001` or `http://localhost:5000`

The SQLite database is created automatically with seed data on first run.

## Project Structure

```
F1RaceTracker/
├── Controllers/
│   ├── HomeController.cs        # Dashboard
│   ├── RacesController.cs       # Race CRUD + Results
│   ├── DriversController.cs     # Driver CRUD
│   └── TeamsController.cs       # Team CRUD
├── Models/
│   ├── Team.cs
│   ├── Driver.cs
│   ├── Race.cs
│   ├── RaceResult.cs
│   └── ViewModels.cs
├── Data/
│   └── AppDbContext.cs          # EF Core context + seed data
├── Views/
│   ├── Home/Index.cshtml        # Dashboard
│   ├── Races/                   # Race views
│   ├── Drivers/                 # Driver views
│   ├── Teams/                   # Team views
│   └── Shared/_Layout.cshtml   # Main layout
└── wwwroot/css/site.css        # F1-themed dark styles
```

## Data Model

```
Team ──< Driver ──< RaceResult >── Race
```

- A **Team** has many **Drivers**
- A **Driver** has many **RaceResults**
- A **Race** has many **RaceResults**

## Extending the App

- Add Authentication with ASP.NET Core Identity
- Add Sprint Race support (separate Sprint results)
- Add Qualifying results tracking
- Connect to the Ergast F1 API for live data
- Add charts with Chart.js for standings visualization

