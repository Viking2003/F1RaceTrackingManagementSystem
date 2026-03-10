namespace F1RaceTracker.Models;

public class RaceResult
{
    public int Id { get; set; }
    public int RaceId { get; set; }
    public Race? Race { get; set; }
    public int DriverId { get; set; }
    public Driver? Driver { get; set; }
    public int Position { get; set; }         // Finishing position (0 = DNF)
    public int GridPosition { get; set; }     // Starting grid position
    public int Points { get; set; }
    public string? FastestLap { get; set; }   // e.g. "1:23.456"
    public bool HasFastestLapPoint { get; set; }
    public string? TotalRaceTime { get; set; }
    public bool DidNotFinish { get; set; }
    public string? DNFReason { get; set; }
    public int PitStops { get; set; }

    public string PositionDisplay => DidNotFinish ? "DNF" : Position.ToString();
}
