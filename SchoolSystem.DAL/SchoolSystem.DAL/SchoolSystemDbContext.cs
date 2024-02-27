using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;
public class SchoolSystemDbContext : DbContext
{
    private readonly bool _seedDemoData;
    public SchoolSystemDbContext(DbContextOptions<SchoolSystemDbContext> options, bool seedDemoData = false) : base(options)
    {
        		_seedDemoData = seedDemoData;
    }

    public DbSet<StudentEntity> Students => Set<StudentEntity>();
    public DbSet<SubjectEntity> Subjects => Set<SubjectEntity>();
    public DbSet<EvaluationEntity> Evaluations => Set<EvaluationEntity>();
    public DbSet<ActivityEntity> Activities => Set<ActivityEntity>();

}
