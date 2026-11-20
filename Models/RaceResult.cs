using System.ComponentModel.DataAnnotations;

namespace F1RaceTracker.Models;

public class RaceResult
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Race is required")]
    public int RaceId { get; set; }
    public Race? Race { get; set; }

    [Required(ErrorMessage = "Driver is required")]
    public int DriverId { get; set; }
    public Driver? Driver { get; set; }

    [Range(0, 20, ErrorMessage = "Position must be between 0 and 20")]
    public int Position { get; set; }         // Finishing position (0 = DNF)

    [Range(1, 20, ErrorMessage = "Grid Position must be between 1 and 20")]
    public int GridPosition { get; set; }     // Starting grid position

    public int Points { get; set; }
    public string? FastestLap { get; set; }   // e.g. "1:23.456"
    public bool HasFastestLapPoint { get; set; }
    public string? TotalRaceTime { get; set; }
    public bool DidNotFinish { get; set; }
    public string? DNFReason { get; set; }

    [Range(0, 10, ErrorMessage = "Pit Stops must be between 0 and 10")]
    public int PitStops { get; set; }

    public string PositionDisplay => DidNotFinish ? "DNF" : Position.ToString();
}
