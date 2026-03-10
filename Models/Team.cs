namespace F1RaceTracker.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PrincipalName { get; set; } = string.Empty;
    public string CarName { get; set; } = string.Empty;
    public string ColorHex { get; set; } = "#FF0000";
    public int FoundedYear { get; set; }
    public int Championships { get; set; }

    public ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}
