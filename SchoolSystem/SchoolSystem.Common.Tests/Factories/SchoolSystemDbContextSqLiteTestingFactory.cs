using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using SchoolSystem.DAL;

namespace SchoolSystem.Common.Tests.Factories;

public class SchoolSystemDbContextSqLiteTestingFactory(string databaseName, bool seedTestingData = false) : IDbContextFactory<SchoolSystemDbContext>
{
    public SchoolSystemDbContext CreateDbContext()
    {
        
        DbContextOptionsBuilder<SchoolSystemDbContext> builder = new();
        builder.UseSqlite($"Data Source={databaseName};Cache=Shared");

        return new SchoolSystemTestingDbContext(builder.Options, seedTestingData);
    }
}