using SchoolSystem.DAL;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Common.Tests.Seeds;

namespace SchoolSystem.Common.Tests;
public class SchoolSystemTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
    : SchoolSystemDbContext(contextOptions, seedDemoData: false)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (seedTestingData)
        {
            StudentSeeds.Seed(modelBuilder);
            SubjectSeeds.Seed(modelBuilder);
            ActivitySeeds.Seed(modelBuilder);
            EvaluationSeeds.Seed(modelBuilder);
        }
    }
}