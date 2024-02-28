using CookBook.Common.Tests.Seeds;
using CookBook.DAL;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Common.Tests;
public class SchoolSystemTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
    : SchoolSystemDbContext(contextOptions, seedDemoData: false)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (seedTestingData)
        {
            //Students....Seed(modelBuilder);

            // add seed data
        }
    }
}