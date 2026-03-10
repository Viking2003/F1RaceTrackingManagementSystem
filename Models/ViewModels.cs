namespace F1RaceTracker.Models;

public class DashboardViewModel
{
    public int TotalRaces { get; set; }
    public int TotalDrivers { get; set; }
    public int TotalTeams { get; set; }
    public int CurrentSeason { get; set; }
    public Race? NextRace { get; set; }
    public Race? LastRace { get; set; }
    public List<DriverStanding> DriverStandings { get; set; } = new();
    public List<TeamStanding> TeamStandings { get; set; } = new();
    public List<Race> UpcomingRaces { get; set; } = new();
    public List<Race> RecentRaces { get; set; } = new();
}

public class DriverStanding
{
    public Driver Driver { get; set; } = null!;
    public int Points { get; set; }
    public int Position { get; set; }
    public int Wins { get; set; }
    public int Podiums { get; set; }
}

public class TeamStanding
{
    public Team Team { get; set; } = null!;
    public int Points { get; set; }
    public int Position { get; set; }
    public int Wins { get; set; }
}

public class RaceDetailViewModel
{
    public Race Race { get; set; } = null!;
    public List<RaceResult> Results { get; set; } = new();
    public RaceResult? Winner => Results.FirstOrDefault(r => r.Position == 1 && !r.DidNotFinish);
    public RaceResult? FastestLapHolder => Results.FirstOrDefault(r => r.HasFastestLapPoint);
}
