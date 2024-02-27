
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SchoolSystem.DAL;
public class SchoolSystemDbContext : DbContext
{
    private readonly bool _seedDemoData;
    public SchoolSystemDbContext(DbContextOptions<SchoolSystemDbContext> options, bool seedDemoData = false) : base(options)
    {
        		_seedDemoData = seedDemoData;
    }

    public DbSet<StudentEntity> Students => Set<StudenStudentEntityt>();
    public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
    public DbSet<EvaluationEntity> Evaluations => Set<EvaluationEntity>();
    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

}
