using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace SchoolSystem.Common.Tests;

public class SchoolSystemDbContextSqLiteTestingFactory(string databaseName, bool seedTestingData = false) : IDbContextFactory<SchoolSystemDbContext>
{
    public static SchoolSystemDbContext CreateDbContext()
    {
        //
        DbContextOptionsBuilder<SchoolSystemDbContext> builder = new();
        builder.UseSqlite($"Data Source={databaseName};Cache=Shared");

        return SchoolSystemTestingDbContext(builder.Options, seedTestingData);
    }
}