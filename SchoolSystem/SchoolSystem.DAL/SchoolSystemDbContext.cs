using DAL.Entities;
using DAL.Seeds;
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
            .OnDelete(DeleteBehavior.Restrict);
        
        //

        modelBuilder.Entity<EvaluationEntity>()
            .HasOne(e => e.Activity)
            .WithMany(a => a.Evaluations)
            .HasForeignKey(e => e.ActivityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EvaluationEntity>()
            .HasOne(e => e.Student)
            .WithMany()
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //

        modelBuilder.Entity<SubjectEntity>()
            .HasMany(s => s.Activities)
            .WithOne(e => e.Subject)
            .OnDelete(DeleteBehavior.Cascade);

        /*modelBuilder.Entity<CourseEntityStudentEntity>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(cs => cs.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseEntityStudentEntity>()
                .HasOne(cs => cs.Student)
                .WithMany()
                .HasForeignKey(cs => cs.StudentId)
                .OnDelete(DeleteBehavior.Restrict); */

        // modelBuilder.Entity<ActivityEntity>()
        //     .HasMany(s => s.Evaluations)
        //     .WithOne(e => e.Activity)
        //     .OnDelete(DeleteBehavior.Cascade);
        // modelBuilder.Entity<EvaluationEntity>()
        //     .HasOne(s => s.Student);
        if (_seedDemoData)
        {
            ActivitySeeds.Seed(modelBuilder);	
            StudentSeeds.Seed(modelBuilder);
            SubjectSeeds.Seed(modelBuilder);
            EvaluationSeeds.Seed(modelBuilder);
        }
    }

}
