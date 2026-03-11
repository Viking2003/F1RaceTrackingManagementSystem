using Microsoft.EntityFrameworkCore;
using F1RaceTracker.Data;

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllersWithViews();

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (databaseUrl != null)
{
    string connectionString;

    // Handle both standard URL and Render's internal URL formats
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var dbPort = uri.Port > 0 ? uri.Port : 5432; // default to 5432 if port is missing

    connectionString = $"Host={uri.Host};Port={dbPort};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";

    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseNpgsql(connectionString));
}
else
{
    // Fallback to SQLite for local development
    var dbPath = Path.Combine(Path.GetTempPath(), "f1tracker.db");
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlite($"Data Source={dbPath}"));
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();