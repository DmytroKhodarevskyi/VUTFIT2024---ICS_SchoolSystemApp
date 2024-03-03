using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.DAL;
public class SchoolSystemDbContext : DbContext
{
    private readonly bool _seedDemoData;
    public SchoolSystemDbContext(DbContextOptions options, bool seedDemoData = false) : base(options)
    {
        _seedDemoData = seedDemoData;
    }

    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
    public DbSet<EvaluationEntity> Evaluations => Set<EvaluationEntity>();
    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<StudentEntity>()
            .HasMany(s => s.Subjects)
            .WithMany(e => e.Students);

        modelBuilder.Entity<SubjectEntity>()
            .HasMany(s => s.Activities)
            .WithOne(e => e.Subject)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ActivityEntity>()
            .HasMany(s => s.Evaluations)
            .WithOne(e => e.Activity)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<EvaluationEntity>()
            .HasOne(s => s.Student);
        // if (seedDemoData) {
        // }
    }

}
