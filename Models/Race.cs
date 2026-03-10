namespace F1RaceTracker.Models;

public class Race
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Circuit { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime RaceDate { get; set; }
    public int Season { get; set; }
    public int Round { get; set; }
    public int Laps { get; set; }
    public string CircuitLength { get; set; } = string.Empty; // km
    public RaceStatus Status { get; set; } = RaceStatus.Scheduled;

    public ICollection<RaceResult> Results { get; set; } = new List<RaceResult>();
}

public enum RaceStatus
{
    Scheduled,
    InProgress,
    Completed,
    Cancelled
}
