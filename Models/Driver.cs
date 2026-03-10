namespace F1RaceTracker.Models;

public class Driver
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public int DriverNumber { get; set; }
    public string Code { get; set; } = string.Empty; // e.g. HAM, VER
    public DateTime DateOfBirth { get; set; }
    public int TeamId { get; set; }
    public Team? Team { get; set; }
    public int Championships { get; set; }
    public int TotalWins { get; set; }
    public int TotalPodiums { get; set; }

    public string FullName => $"{FirstName} {LastName}";
    public ICollection<RaceResult> RaceResults { get; set; } = new List<RaceResult>();
}
