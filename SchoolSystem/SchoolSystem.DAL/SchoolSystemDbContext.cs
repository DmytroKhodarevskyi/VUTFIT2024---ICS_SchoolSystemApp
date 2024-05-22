using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Common.Tests.Seeds;

namespace SchoolSystem.DAL;
public class SchoolSystemDbContext(DbContextOptions options, bool seedDemoData = false) : DbContext(options)
{
    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
    public DbSet<EvaluationEntity> Evaluations => Set<EvaluationEntity>();
    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ActivityEntity>().HasKey(a => a.Id);
        modelBuilder.Entity<SubjectEntity>().HasKey(s => s.Id);
        modelBuilder.Entity<EvaluationEntity>().HasKey(e => e.Id);
        modelBuilder.Entity<StudentEntity>().HasKey(s => s.Id);
        
        modelBuilder.Entity<StudentEntity>()
            .HasMany<EvaluationEntity>()
            .WithOne(s => s.Student);
        
        modelBuilder.Entity<ActivityEntity>()
            .HasOne(a => a.Subject)
            .WithMany(c => c.Activities)
            .HasForeignKey(a => a.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<EvaluationEntity>()
            .HasOne(e => e.Activity)
            .WithMany(a => a.Evaluations)
            .HasForeignKey(e => e.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EvaluationEntity>()
            .HasOne(e => e.Student)
            .WithMany()
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<SubjectEntity>()
            .HasMany(s => s.Activities)
            .WithOne(e => e.Subject)
            .OnDelete(DeleteBehavior.Cascade);
        
        if (seedDemoData)
        {
            ActivitySeeds.Seed(modelBuilder);	
            StudentSeeds.Seed(modelBuilder);
            SubjectSeeds.Seed(modelBuilder);
            EvaluationSeeds.Seed(modelBuilder);
        }
    }
}
