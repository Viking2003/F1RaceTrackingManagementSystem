using System.ComponentModel.DataAnnotations;

namespace F1RaceTracker.Models;

public class Race
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Race Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Circuit is required")]
    public string Circuit { get; set; } = string.Empty;

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Race Date is required")]
    public DateTime RaceDate { get; set; }

    [Required(ErrorMessage = "Season is required")]
    [Range(1950, 2100, ErrorMessage = "Season must be between 1950 and 2100")]
    public int Season { get; set; }

    [Required(ErrorMessage = "Round is required")]
    [Range(1, 30, ErrorMessage = "Round must be between 1 and 30")]
    public int Round { get; set; }

    [Required(ErrorMessage = "Laps is required")]
    [Range(1, 100, ErrorMessage = "Laps must be between 1 and 100")]
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
