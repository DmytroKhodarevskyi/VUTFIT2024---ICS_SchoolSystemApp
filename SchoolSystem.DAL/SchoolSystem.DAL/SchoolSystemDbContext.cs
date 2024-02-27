namespace SchoolSystem.DAL;

using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class SchoolSystemDbContext : DbContext
{
    private readonly bool _seedDemoData;
    public SchoolSystemDbContext(DbContextOptions<SchoolSystemDbContext> options, bool seedDemoData = false) : base(options)
    {
        		_seedDemoData = seedDemoData;
    }

    public DbSet<StudentEntity> Students => Set<StudenStudentEntityt>();
    public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
    public DbSet<EvaluationEntity> Evaluation => Set<EvaluationEntity>();
    public DbSet<ActivityEntity> SubjectStudents => Set<ActivityEntity>();

}
