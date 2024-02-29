using DAL.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Common.Tests.Seeds;

public static class SubjectSeeds
{
    public static readonly SubjectEntity EmptySubject = new()
    {
        Id = default,
        Name = default!,
        Abbreviation = default!,
    };
    
    public static readonly SubjectEntity IZP = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-0000000000089"),
        Name = "IZP",
        Abbreviation = "IZP",
    };
    
    public static readonly SubjectEntity IUS = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-0000000000078"),
        Name = "IUS",
        Abbreviation = "IUS",
    };
    
    public static readonly SubjectEntity SubjectEntityWithNoStudAct = IZP with 
        { Id = Guid.Parse("00000000-0000-0000-0000-0000000000067"), 
            Students = Array.Empty<StudentEntity>(), 
            Activities = Array.Empty<ActivityEntity>()};
    
    public static readonly SubjectEntity SubjectEntityUpdated = IZP with { Id = Guid.Parse("00000000-0000-0000-0000-0000000000056"),
        Students = Array.Empty<StudentEntity>(), 
        Activities = Array.Empty<ActivityEntity>()};
    public static readonly SubjectEntity SubjectEntityDeleted = IZP with { Id = Guid.Parse("00000000-0000-0000-0000-0000000000045"),
        Students = Array.Empty<StudentEntity>(), 
        Activities = Array.Empty<ActivityEntity>()};
    public  static readonly SubjectEntity SubjectEntityUpdate2 = IUS with { Id = Guid.Parse("00000000-0000-0000-0000-0000000000034"),
        Students = Array.Empty<StudentEntity>(), 
        Activities = Array.Empty<ActivityEntity>()};
    public static readonly SubjectEntity SubjectEntityDelete2 = IUS with { Id = Guid.Parse("00000000-0000-0000-0000-0000000000023"),
        Students = Array.Empty<StudentEntity>(), 
        Activities = Array.Empty<ActivityEntity>()};

    static SubjectSeeds()
    {
        IZP.Students.Add(StudentSeeds.Student1);
        IUS.Students.Add(StudentSeeds.Student1);
        IZP.Activities.Add(ActivitySeeds.Activity1);
        IUS.Activities.Add(ActivitySeeds.Activity2);
    }
    
    public static void Seed(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<SubjectEntity>().HasData(
            IZP, IUS with{ Students = Array.Empty<StudentEntity>(), Activities = Array.Empty<ActivityEntity>()},
            SubjectEntityWithNoStudAct,
            SubjectEntityUpdated,
            SubjectEntityDeleted,
            SubjectEntityUpdate2,
            SubjectEntityDelete2
            );
    
}
