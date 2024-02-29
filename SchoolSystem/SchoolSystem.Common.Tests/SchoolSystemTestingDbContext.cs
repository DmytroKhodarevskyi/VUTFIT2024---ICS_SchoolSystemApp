using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL;
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
            StudentSeed.Seed(modelBuilder);
            SubjectSeed.Seed(modelBuilder);
            ActivitySeed.Seed(modelBuilder);
            EvaluationSeed.Seed(modelBuilder);
        }
    }
}