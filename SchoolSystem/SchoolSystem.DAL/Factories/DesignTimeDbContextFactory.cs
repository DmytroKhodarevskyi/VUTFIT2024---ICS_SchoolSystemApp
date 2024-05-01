using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SchoolSystem.DAL;

namespace DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SchoolSystemDbContext>
{
    private readonly DbContextSqLiteFactory _dbContextSqLiteFactory = new($"Data Source=SchoolSystem;Cache=Shared");

    public SchoolSystemDbContext CreateDbContext(string[] args) => _dbContextSqLiteFactory.CreateDbContext();
}