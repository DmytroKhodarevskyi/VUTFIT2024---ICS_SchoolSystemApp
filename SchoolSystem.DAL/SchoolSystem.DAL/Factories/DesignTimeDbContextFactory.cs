using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SchoolSystem.DAL.Factories;

/// <summary>
///     EF Core CLI migration generation uses this DbContext to create model and migration
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SchoolSystemDbContext>
{
    private readonly DbContextSqLiteFactory _dbContextSqLiteFactory = new($"Data Source=SchoolSystem;Cache=Shared");

    public SchoolSystemDbContext CreateDbContext(string[] args) => _dbContextSqLiteFactory.CreateDbContext();
}