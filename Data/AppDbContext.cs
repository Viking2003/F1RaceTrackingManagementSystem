using Microsoft.EntityFrameworkCore;
using F1RaceTracker.Models;

namespace F1RaceTracker.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Race> Races => Set<Race>();
    public DbSet<RaceResult> RaceResults => Set<RaceResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RaceResult>()
            .HasOne(r => r.Race)
            .WithMany(r => r.Results)
            .HasForeignKey(r => r.RaceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RaceResult>()
            .HasOne(r => r.Driver)
            .WithMany(d => d.RaceResults)
            .HasForeignKey(r => r.DriverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Driver>()
            .HasOne(d => d.Team)
            .WithMany(t => t.Drivers)
            .HasForeignKey(d => d.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder mb)
    {
        // Teams
        mb.Entity<Team>().HasData(
            new Team { Id = 1, Name = "Red Bull Racing", Country = "Austria", PrincipalName = "Christian Horner", CarName = "RB20", ColorHex = "#3671C6", FoundedYear = 2005, Championships = 6 },
            new Team { Id = 2, Name = "Ferrari", Country = "Italy", PrincipalName = "Frédéric Vasseur", CarName = "SF-24", ColorHex = "#E8002D", FoundedYear = 1950, Championships = 16 },
            new Team { Id = 3, Name = "Mercedes", Country = "Germany", PrincipalName = "Toto Wolff", CarName = "W15", ColorHex = "#27F4D2", FoundedYear = 2010, Championships = 8 },
            new Team { Id = 4, Name = "McLaren", Country = "United Kingdom", PrincipalName = "Andrea Stella", CarName = "MCL38", ColorHex = "#FF8000", FoundedYear = 1966, Championships = 8 },
            new Team { Id = 5, Name = "Aston Martin", Country = "United Kingdom", PrincipalName = "Mike Krack", CarName = "AMR24", ColorHex = "#229971", FoundedYear = 2021, Championships = 0 },
            new Team { Id = 6, Name = "Alpine", Country = "France", PrincipalName = "Bruno Famin", CarName = "A524", ColorHex = "#0093CC", FoundedYear = 2021, Championships = 0 },
            new Team { Id = 7, Name = "Williams", Country = "United Kingdom", PrincipalName = "James Vowles", CarName = "FW46", ColorHex = "#64C4FF", FoundedYear = 1977, Championships = 7 },
            new Team { Id = 8, Name = "Haas", Country = "United States", PrincipalName = "Ayao Komatsu", CarName = "VF-24", ColorHex = "#B6BABD", FoundedYear = 2016, Championships = 0 },
            new Team { Id = 9, Name = "RB", Country = "Italy", PrincipalName = "Laurent Mekies", CarName = "VCARB 01", ColorHex = "#6692FF", FoundedYear = 2024, Championships = 0 },
            new Team { Id = 10, Name = "Kick Sauber", Country = "Switzerland", PrincipalName = "Alessandro Alunni Bravi", CarName = "C44", ColorHex = "#52E252", FoundedYear = 1993, Championships = 0 }
        );

        // Drivers
        mb.Entity<Driver>().HasData(
            new Driver { Id = 1, FirstName = "Max", LastName = "Verstappen", Nationality = "Dutch", DriverNumber = 1, Code = "VER", DateOfBirth = new DateTime(1997, 9, 30), TeamId = 1, Championships = 4, TotalWins = 62, TotalPodiums = 107 },
            new Driver { Id = 2, FirstName = "Sergio", LastName = "Perez", Nationality = "Mexican", DriverNumber = 11, Code = "PER", DateOfBirth = new DateTime(1990, 1, 26), TeamId = 1, Championships = 0, TotalWins = 6, TotalPodiums = 40 },
            new Driver { Id = 3, FirstName = "Charles", LastName = "Leclerc", Nationality = "Monegasque", DriverNumber = 16, Code = "LEC", DateOfBirth = new DateTime(1997, 10, 16), TeamId = 2, Championships = 0, TotalWins = 8, TotalPodiums = 41 },
            new Driver { Id = 4, FirstName = "Carlos", LastName = "Sainz", Nationality = "Spanish", DriverNumber = 55, Code = "SAI", DateOfBirth = new DateTime(1994, 9, 1), TeamId = 2, Championships = 0, TotalWins = 3, TotalPodiums = 25 },
            new Driver { Id = 5, FirstName = "Lewis", LastName = "Hamilton", Nationality = "British", DriverNumber = 44, Code = "HAM", DateOfBirth = new DateTime(1985, 1, 7), TeamId = 3, Championships = 7, TotalWins = 103, TotalPodiums = 197 },
            new Driver { Id = 6, FirstName = "George", LastName = "Russell", Nationality = "British", DriverNumber = 63, Code = "RUS", DateOfBirth = new DateTime(1998, 2, 15), TeamId = 3, Championships = 0, TotalWins = 2, TotalPodiums = 14 },
            new Driver { Id = 7, FirstName = "Lando", LastName = "Norris", Nationality = "British", DriverNumber = 4, Code = "NOR", DateOfBirth = new DateTime(2000, 11, 13), TeamId = 4, Championships = 0, TotalWins = 4, TotalPodiums = 23 },
            new Driver { Id = 8, FirstName = "Oscar", LastName = "Piastri", Nationality = "Australian", DriverNumber = 81, Code = "PIA", DateOfBirth = new DateTime(2001, 4, 6), TeamId = 4, Championships = 0, TotalWins = 2, TotalPodiums = 11 },
            new Driver { Id = 9, FirstName = "Fernando", LastName = "Alonso", Nationality = "Spanish", DriverNumber = 14, Code = "ALO", DateOfBirth = new DateTime(1981, 7, 29), TeamId = 5, Championships = 2, TotalWins = 32, TotalPodiums = 106 },
            new Driver { Id = 10, FirstName = "Lance", LastName = "Stroll", Nationality = "Canadian", DriverNumber = 18, Code = "STR", DateOfBirth = new DateTime(1998, 10, 29), TeamId = 5, Championships = 0, TotalWins = 0, TotalPodiums = 3 }
        );

        // 2024 Races
        mb.Entity<Race>().HasData(
            new Race { Id = 1, Name = "Bahrain Grand Prix", Circuit = "Bahrain International Circuit", Country = "Bahrain", City = "Sakhir", RaceDate = new DateTime(2024, 3, 2), Season = 2024, Round = 1, Laps = 57, CircuitLength = "5.412 km", Status = RaceStatus.Completed },
            new Race { Id = 2, Name = "Saudi Arabian Grand Prix", Circuit = "Jeddah Corniche Circuit", Country = "Saudi Arabia", City = "Jeddah", RaceDate = new DateTime(2024, 3, 9), Season = 2024, Round = 2, Laps = 50, CircuitLength = "6.174 km", Status = RaceStatus.Completed },
            new Race { Id = 3, Name = "Australian Grand Prix", Circuit = "Albert Park Circuit", Country = "Australia", City = "Melbourne", RaceDate = new DateTime(2024, 3, 24), Season = 2024, Round = 3, Laps = 58, CircuitLength = "5.278 km", Status = RaceStatus.Completed },
            new Race { Id = 4, Name = "Japanese Grand Prix", Circuit = "Suzuka International Racing Course", Country = "Japan", City = "Suzuka", RaceDate = new DateTime(2024, 4, 7), Season = 2024, Round = 4, Laps = 53, CircuitLength = "5.807 km", Status = RaceStatus.Completed },
            new Race { Id = 5, Name = "Chinese Grand Prix", Circuit = "Shanghai International Circuit", Country = "China", City = "Shanghai", RaceDate = new DateTime(2024, 4, 21), Season = 2024, Round = 5, Laps = 56, CircuitLength = "5.451 km", Status = RaceStatus.Completed },
            new Race { Id = 6, Name = "Monaco Grand Prix", Circuit = "Circuit de Monaco", Country = "Monaco", City = "Monte Carlo", RaceDate = new DateTime(2024, 5, 26), Season = 2024, Round = 8, Laps = 78, CircuitLength = "3.337 km", Status = RaceStatus.Completed },
            new Race { Id = 7, Name = "British Grand Prix", Circuit = "Silverstone Circuit", Country = "United Kingdom", City = "Silverstone", RaceDate = new DateTime(2024, 7, 7), Season = 2024, Round = 12, Laps = 52, CircuitLength = "5.891 km", Status = RaceStatus.Completed },
            new Race { Id = 8, Name = "Italian Grand Prix", Circuit = "Autodromo Nazionale Monza", Country = "Italy", City = "Monza", RaceDate = new DateTime(2024, 9, 1), Season = 2024, Round = 16, Laps = 53, CircuitLength = "5.793 km", Status = RaceStatus.Completed },
            new Race { Id = 9, Name = "Singapore Grand Prix", Circuit = "Marina Bay Street Circuit", Country = "Singapore", City = "Singapore", RaceDate = new DateTime(2024, 9, 22), Season = 2024, Round = 18, Laps = 62, CircuitLength = "4.940 km", Status = RaceStatus.Completed },
            new Race { Id = 10, Name = "Abu Dhabi Grand Prix", Circuit = "Yas Marina Circuit", Country = "UAE", City = "Abu Dhabi", RaceDate = new DateTime(2024, 12, 8), Season = 2024, Round = 24, Laps = 58, CircuitLength = "5.281 km", Status = RaceStatus.Completed },
            new Race { Id = 11, Name = "Australian Grand Prix", Circuit = "Albert Park Circuit", Country = "Australia", City = "Melbourne", RaceDate = new DateTime(2025, 3, 16), Season = 2025, Round = 1, Laps = 58, CircuitLength = "5.278 km", Status = RaceStatus.Completed },
            new Race { Id = 12, Name = "Chinese Grand Prix", Circuit = "Shanghai International Circuit", Country = "China", City = "Shanghai", RaceDate = new DateTime(2025, 3, 23), Season = 2025, Round = 2, Laps = 56, CircuitLength = "5.451 km", Status = RaceStatus.Completed },
            new Race { Id = 13, Name = "Japanese Grand Prix", Circuit = "Suzuka International Racing Course", Country = "Japan", City = "Suzuka", RaceDate = new DateTime(2025, 4, 6), Season = 2025, Round = 3, Laps = 53, CircuitLength = "5.807 km", Status = RaceStatus.Scheduled },
            new Race { Id = 14, Name = "Bahrain Grand Prix", Circuit = "Bahrain International Circuit", Country = "Bahrain", City = "Sakhir", RaceDate = new DateTime(2025, 4, 13), Season = 2025, Round = 4, Laps = 57, CircuitLength = "5.412 km", Status = RaceStatus.Scheduled },
            new Race { Id = 15, Name = "Saudi Arabian Grand Prix", Circuit = "Jeddah Corniche Circuit", Country = "Saudi Arabia", City = "Jeddah", RaceDate = new DateTime(2025, 4, 20), Season = 2025, Round = 5, Laps = 50, CircuitLength = "6.174 km", Status = RaceStatus.Scheduled }
        );

        // Some race results for 2024 Bahrain GP
        mb.Entity<RaceResult>().HasData(
            new RaceResult { Id = 1, RaceId = 1, DriverId = 1, Position = 1, GridPosition = 1, Points = 25, FastestLap = "1:32.608", HasFastestLapPoint = false, TotalRaceTime = "1:31:44.742", DidNotFinish = false, PitStops = 2 },
            new RaceResult { Id = 2, RaceId = 1, DriverId = 2, Position = 2, GridPosition = 2, Points = 18, FastestLap = "1:33.121", HasFastestLapPoint = false, TotalRaceTime = "+22.457s", DidNotFinish = false, PitStops = 2 },
            new RaceResult { Id = 3, RaceId = 1, DriverId = 3, Position = 3, GridPosition = 4, Points = 15, FastestLap = "1:33.409", HasFastestLapPoint = false, TotalRaceTime = "+25.017s", DidNotFinish = false, PitStops = 2 },
            new RaceResult { Id = 4, RaceId = 1, DriverId = 4, Position = 4, GridPosition = 3, Points = 12, FastestLap = "1:33.612", HasFastestLapPoint = false, TotalRaceTime = "+35.155s", DidNotFinish = false, PitStops = 3 },
            new RaceResult { Id = 5, RaceId = 1, DriverId = 7, Position = 5, GridPosition = 6, Points = 10, FastestLap = "1:33.843", HasFastestLapPoint = false, TotalRaceTime = "+39.003s", DidNotFinish = false, PitStops = 2 },
            new RaceResult { Id = 6, RaceId = 1, DriverId = 5, Position = 6, GridPosition = 8, Points = 8, FastestLap = "1:32.608", HasFastestLapPoint = true, TotalRaceTime = "+46.797s", DidNotFinish = false, PitStops = 2 },
            new RaceResult { Id = 7, RaceId = 1, DriverId = 9, Position = 7, GridPosition = 5, Points = 6, FastestLap = "1:34.102", HasFastestLapPoint = false, TotalRaceTime = "+49.855s", DidNotFinish = false, PitStops = 2 },
            new RaceResult { Id = 8, RaceId = 1, DriverId = 6, Position = 8, GridPosition = 7, Points = 4, FastestLap = "1:34.512", HasFastestLapPoint = false, TotalRaceTime = "+56.183s", DidNotFinish = false, PitStops = 2 },
            new RaceResult { Id = 9, RaceId = 1, DriverId = 8, Position = 9, GridPosition = 9, Points = 2, FastestLap = "1:34.701", HasFastestLapPoint = false, TotalRaceTime = "+63.410s", DidNotFinish = false, PitStops = 2 },
            new RaceResult { Id = 10, RaceId = 1, DriverId = 10, Position = 10, GridPosition = 10, Points = 1, FastestLap = "1:35.201", HasFastestLapPoint = false, TotalRaceTime = "+70.822s", DidNotFinish = false, PitStops = 3 },
            // Monaco GP
            new RaceResult { Id = 11, RaceId = 6, DriverId = 3, Position = 1, GridPosition = 1, Points = 25, FastestLap = "1:14.820", HasFastestLapPoint = false, TotalRaceTime = "1:57:46.374", DidNotFinish = false, PitStops = 1 },
            new RaceResult { Id = 12, RaceId = 6, DriverId = 4, Position = 2, GridPosition = 2, Points = 18, FastestLap = "1:14.956", HasFastestLapPoint = false, TotalRaceTime = "+7.152s", DidNotFinish = false, PitStops = 1 },
            new RaceResult { Id = 13, RaceId = 6, DriverId = 7, Position = 3, GridPosition = 4, Points = 15, FastestLap = "1:15.101", HasFastestLapPoint = false, TotalRaceTime = "+15.609s", DidNotFinish = false, PitStops = 1 },
            // British GP
            new RaceResult { Id = 14, RaceId = 7, DriverId = 7, Position = 1, GridPosition = 2, Points = 25, FastestLap = "1:27.097", HasFastestLapPoint = true, TotalRaceTime = "1:22:27.187", DidNotFinish = false, PitStops = 2 },
            new RaceResult { Id = 15, RaceId = 7, DriverId = 1, Position = 2, GridPosition = 1, Points = 18, FastestLap = "1:27.356", HasFastestLapPoint = false, TotalRaceTime = "+1.465s", DidNotFinish = false, PitStops = 2 },
            new RaceResult { Id = 16, RaceId = 7, DriverId = 5, Position = 3, GridPosition = 5, Points = 15, FastestLap = "1:27.612", HasFastestLapPoint = false, TotalRaceTime = "+5.481s", DidNotFinish = false, PitStops = 3 }
        );
    }
}
