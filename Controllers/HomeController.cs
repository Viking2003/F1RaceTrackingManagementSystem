using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F1RaceTracker.Data;
using F1RaceTracker.Models;

namespace F1RaceTracker.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var now = DateTime.UtcNow;
        var currentSeason = now.Year;

        var allResults = await _db.RaceResults
            .Include(r => r.Driver).ThenInclude(d => d!.Team)
            .Include(r => r.Race)
            .Where(r => r.Race!.Season == currentSeason)
            .ToListAsync();

        // Driver standings
        var driverStandings = allResults
            .Where(r => r.Driver != null)
            .GroupBy(r => r.Driver!)
            .Select(g => new DriverStanding
            {
                Driver = g.Key,
                Points = g.Sum(r => r.Points),
                Wins = g.Count(r => r.Position == 1 && !r.DidNotFinish),
                Podiums = g.Count(r => r.Position <= 3 && !r.DidNotFinish)
            })
            .OrderByDescending(d => d.Points).ThenByDescending(d => d.Wins)
            .Select((d, i) => { d.Position = i + 1; return d; })
            .ToList();

        // Team standings
        var teamStandings = allResults
            .Where(r => r.Driver?.Team != null)
            .GroupBy(r => r.Driver!.Team!)
            .Select(g => new TeamStanding
            {
                Team = g.Key,
                Points = g.Sum(r => r.Points),
                Wins = g.Count(r => r.Position == 1 && !r.DidNotFinish)
            })
            .OrderByDescending(t => t.Points).ThenByDescending(t => t.Wins)
            .Select((t, i) => { t.Position = i + 1; return t; })
            .ToList();

        var races = await _db.Races
            .Where(r => r.Season == currentSeason)
            .OrderBy(r => r.Round)
            .ToListAsync();

        var vm = new DashboardViewModel
        {
            TotalRaces = await _db.Races.CountAsync(),
            TotalDrivers = await _db.Drivers.CountAsync(),
            TotalTeams = await _db.Teams.CountAsync(),
            CurrentSeason = currentSeason,
            DriverStandings = driverStandings.Take(10).ToList(),
            TeamStandings = teamStandings.Take(10).ToList(),
            NextRace = races.FirstOrDefault(r => r.RaceDate > now),
            LastRace = races.LastOrDefault(r => r.Status == RaceStatus.Completed),
            UpcomingRaces = races.Where(r => r.RaceDate > now).Take(3).ToList(),
            RecentRaces = races.Where(r => r.Status == RaceStatus.Completed).TakeLast(3).Reverse().ToList()
        };

        return View(vm);
    }
}
