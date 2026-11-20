using System.ComponentModel.DataAnnotations;

namespace F1RaceTracker.Models;

public class Driver
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nationality is required")]
    public string Nationality { get; set; } = string.Empty;

    [Required(ErrorMessage = "Driver Number is required")]
    [Range(1, 99, ErrorMessage = "Driver Number must be between 1 and 99")]
    public int DriverNumber { get; set; }

    [Required(ErrorMessage = "Driver Code is required")]
    [StringLength(3, MinimumLength = 3, ErrorMessage = "Driver Code must be exactly 3 characters")]
    public string Code { get; set; } = string.Empty; // e.g. HAM, VER

    [Required(ErrorMessage = "Date of Birth is required")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Team is required")]
    public int TeamId { get; set; }
    public Team? Team { get; set; }
    public int Championships { get; set; }
    public int TotalWins { get; set; }
    public int TotalPodiums { get; set; }

    public string FullName => $"{FirstName} {LastName}";
    public ICollection<RaceResult> RaceResults { get; set; } = new List<RaceResult>();
}
