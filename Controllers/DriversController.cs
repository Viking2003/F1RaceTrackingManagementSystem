using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using F1RaceTracker.Data;
using F1RaceTracker.Models;

namespace F1RaceTracker.Controllers;

public class DriversController : Controller
{
    private readonly AppDbContext _db;

    public DriversController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var drivers = await _db.Drivers
            .Include(d => d.Team)
            .OrderBy(d => d.LastName)
            .ToListAsync();
        return View(drivers);
    }

    public async Task<IActionResult> Details(int id)
    {
        var driver = await _db.Drivers
            .Include(d => d.Team)
            .Include(d => d.RaceResults).ThenInclude(r => r.Race)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (driver == null) return NotFound();

        var results = driver.RaceResults
            .Where(r => r.Race != null)
            .OrderByDescending(r => r.Race!.RaceDate)
            .ToList();

        ViewBag.Results = results;
        ViewBag.SeasonPoints = results
            .Where(r => r.Race!.Season == DateTime.UtcNow.Year)
            .Sum(r => r.Points);
        ViewBag.SeasonWins = results
            .Where(r => r.Race!.Season == DateTime.UtcNow.Year && r.Position == 1 && !r.DidNotFinish)
            .Count();

        return View(driver);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Teams = await _db.Teams.OrderBy(t => t.Name).ToListAsync();
        return View(new Driver());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Driver driver)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Teams = await _db.Teams.OrderBy(t => t.Name).ToListAsync();
            return View(driver);
        }
        _db.Drivers.Add(driver);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"Driver '{driver.FullName}' added!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var driver = await _db.Drivers.FindAsync(id);
        if (driver == null) return NotFound();
        ViewBag.Teams = await _db.Teams.OrderBy(t => t.Name).ToListAsync();
        return View(driver);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Driver driver)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Teams = await _db.Teams.OrderBy(t => t.Name).ToListAsync();
            return View(driver);
        }
        _db.Drivers.Update(driver);
        await _db.SaveChangesAsync();
        TempData["Success"] = $"Driver '{driver.FullName}' updated!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var driver = await _db.Drivers.FindAsync(id);
        if (driver != null)
        {
            _db.Drivers.Remove(driver);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Driver deleted.";
        }
        return RedirectToAction(nameof(Index));
    }
}
