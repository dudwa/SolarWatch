using Microsoft.EntityFrameworkCore;

namespace SolarWatch.Data;


public class SolarWatchAPIContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SolarTime> SolarTimes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=SolarApi;User Id=sa;Password=izagy01*;TrustServerCertificate=true;");
    }
}
