using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<SchoolSystemDbContext>
{
    private readonly bool _seedTestingData;
    private readonly DbContextOptionsBuilder<SchoolSystemDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName, bool seedTestingData = false)
    {
        _seedTestingData = seedTestingData;
        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
    }

    public SchoolSystemDbContext CreateDbContext() => new(_contextOptionsBuilder.Options, _seedTestingData);
}