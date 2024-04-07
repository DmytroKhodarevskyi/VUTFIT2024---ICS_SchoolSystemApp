using DAL.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds;

public static class SubjectSeeds
{
    public static readonly SubjectEntity IZP = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
        Name = "IZP",
        Abbreviation = "IZP",
    };
    
    public static readonly SubjectEntity IUS = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
        Name = "IUS",
        Abbreviation = "IUS",
    };
    
    static SubjectSeeds()
    {
        IZP.Activities.Add(ActivitySeeds.Activity1);
        IUS.Activities.Add(ActivitySeeds.Activity2);
        // IUS.Students.Add(StudentSeeds.Student1);
        // IZP.Students.Add(StudentSeeds.Student1);
    }
    
    public static void Seed(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<SubjectEntity>().HasData(
            IZP with {Activities = Array.Empty<ActivityEntity>(), StudentSubjects = Array.Empty<StudentSubjectEntity>()}, IUS with {Activities = Array.Empty<ActivityEntity>(), StudentSubjects = Array.Empty<StudentSubjectEntity>()});
}    
