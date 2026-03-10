using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F1RaceTracker.Data;
using F1RaceTracker.Models;

namespace F1RaceTracker.Controllers;

public class TeamsController : Controller
{
    private readonly AppDbContext _db;

    public TeamsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var teams = await _db.Teams
            .Include(t => t.Drivers)
            .OrderBy(t => t.Name)
            .ToListAsync();
        return View(teams);
    }

    public async Task<IActionResult> Details(int id)
    {
        var team = await _db.Teams
            .Include(t => t.Drivers).ThenInclude(d => d.RaceResults).ThenInclude(r => r.Race)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (team == null) return NotFound();

        ViewBag.SeasonPoints = team.Drivers
            .SelectMany(d => d.RaceResults)
            .Where(r => r.Race?.Season == DateTime.UtcNow.Year)
            .Sum(r => r.Points);

        return View(team);
    }

    public IActionResult Create() => View(new Team());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Team team)
    {
        if (!ModelState.IsValid) return View(team);
        _db.Teams.Add(team);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"Team '{team.Name}' created!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var team = await _db.Teams.FindAsync(id);
        if (team == null) return NotFound();
        return View(team);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Team team)
    {
        if (!ModelState.IsValid) return View(team);
        _db.Teams.Update(team);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"Team '{team.Name}' updated!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var team = await _db.Teams.FindAsync(id);
        if (team != null)
        {
            _db.Teams.Remove(team);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Team deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
