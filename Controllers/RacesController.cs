using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F1RaceTracker.Data;
using F1RaceTracker.Models;

namespace F1RaceTracker.Controllers;

public class RacesController : Controller
{
    private readonly AppDbContext _db;

    public RacesController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index(int? season)
    {
        season ??= DateTime.UtcNow.Year;
        var races = await _db.Races
            .Where(r => r.Season == season)
            .OrderBy(r => r.Round)
            .ToListAsync();

        var seasons = await _db.Races.Select(r => r.Season).Distinct().OrderByDescending(s => s).ToListAsync();
        ViewBag.Seasons = seasons;
        ViewBag.CurrentSeason = season;
        return View(races);
    }

    public async Task<IActionResult> Details(int id)
    {
        var race = await _db.Races.FindAsync(id);
        if (race == null) return NotFound();

        var results = await _db.RaceResults
            .Include(r => r.Driver).ThenInclude(d => d!.Team)
            .Where(r => r.RaceId == id)
            .OrderBy(r => r.DidNotFinish ? 99 : r.Position)
            .ToListAsync();

        return View(new RaceDetailViewModel { Race = race, Results = results });
    }

    public IActionResult Create() => View(new Race { Season = DateTime.UtcNow.Year });

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Race race)
    {
        if (!ModelState.IsValid) return View(race);
        _db.Races.Add(race);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"Race '{race.Name}' created successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var race = await _db.Races.FindAsync(id);
        if (race == null) return NotFound();
        return View(race);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Race race)
    {
        if (!ModelState.IsValid) return View(race);
        _db.Races.Update(race);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"Race '{race.Name}' updated successfully!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var race = await _db.Races.FindAsync(id);
        if (race != null)
        {
            _db.Races.Remove(race);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Race deleted successfully.";
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> AddResult(int raceId)
    {
        var race = await _db.Races.FindAsync(raceId);
        if (race == null) return NotFound();

        var drivers = await _db.Drivers.Include(d => d.Team).OrderBy(d => d.LastName).ToListAsync();
        ViewBag.Race = race;
        ViewBag.Drivers = drivers;
        return View(new RaceResult { RaceId = raceId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddResult(RaceResult result)
    {
        // Calculate points
        if (!result.DidNotFinish)
        {
            result.Points = result.Position switch
            {
                1 => 25, 2 => 18, 3 => 15, 4 => 12, 5 => 10,
                6 => 8, 7 => 6, 8 => 4, 9 => 2, 10 => 1, _ => 0
            };
            if (result.HasFastestLapPoint && result.Position <= 10) result.Points += 1;
        }
        else
        {
            result.Points = 0;
        }

        _db.RaceResults.Add(result);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Race result added!";
        return RedirectToAction(nameof(Details), new { id = result.RaceId });
    }
}
