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
        Id = Guid.Parse("0d4fa150-ad80-4d46-a51a-4c666166ec5e"),

        Name = "IZP",
        Abbreviation = "IZP",
    };
    
    public static readonly SubjectEntity IUS = new()
    {
        Id = Guid.Parse("0d4fa150-ad80-4da6-a511-4c666166ec5e"),
        Name = "IUS",
        Abbreviation = "IUS",
    };
    
    public static readonly SubjectEntity SubjectEntityWithNoStudAct = IZP with 
        { Id = Guid.Parse("0d4fa150-aa80-4d46-a511-4c666166e12e"),
            StudentSubjects = Array.Empty<StudentSubjectEntity>(), 
            Students = Array.Empty<StudentEntity>(),
            Activities = Array.Empty<ActivityEntity>()};
    
    public static readonly SubjectEntity SubjectEntityUpdated = IZP with { Id = Guid.Parse("0d4fa150-ad30-4d46-a511-4c666166e12e"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>(), 
        Students = Array.Empty<StudentEntity>(),
        Activities = Array.Empty<ActivityEntity>()};
    public static readonly SubjectEntity SubjectEntityDeleted = IZP with { Id = Guid.Parse("0d4fa150-ad20-4d46-a511-4a666166e12e"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>(),
        Students = Array.Empty<StudentEntity>(), 
        Activities = Array.Empty<ActivityEntity>()};
    public  static readonly SubjectEntity SubjectEntityUpdate2 = IUS with { Id = Guid.Parse("0d4fa350-ad10-4d46-a511-4c666166e12e"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>(), 
        Students = Array.Empty<StudentEntity>(), 
        Activities = Array.Empty<ActivityEntity>()};
    public static readonly SubjectEntity SubjectEntityDelete2 = IUS with { Id = Guid.Parse("0d4f1250-ad80-4d46-a511-4c666166e12e"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>(), 
        Students = Array.Empty<StudentEntity>(), 
        Activities = Array.Empty<ActivityEntity>()};

    static SubjectSeeds()
    {
        IZP.Students!.Add(StudentSeeds.Student1);
        IUS.Students!.Add(StudentSeeds.Student1);
        IZP.Activities!.Add(ActivitySeeds.Activity1);
        IUS.Activities!.Add(ActivitySeeds.Activity2);
        // IZP.StudentSubjects!.Add( new StudentSubjectEntity { StudentId = StudentSeeds.Student1.Id, SubjectId = IZP.Id });  
        // IUS.StudentSubjects!.Add( new StudentSubjectEntity { StudentId = StudentSeeds.Student1.Id, SubjectId = IUS.Id });
    }
    
    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<SubjectEntity>().HasData(
            IZP with{ StudentSubjects = Array.Empty<StudentSubjectEntity>(), Students = Array.Empty<StudentEntity>(), Activities = Array.Empty<ActivityEntity>()}, IUS with{ StudentSubjects = Array.Empty<StudentSubjectEntity>(),Students = Array.Empty<StudentEntity>(),  Activities = Array.Empty<ActivityEntity>()},
            SubjectEntityWithNoStudAct,
            SubjectEntityUpdated,
            SubjectEntityDeleted,
            SubjectEntityUpdate2,
            SubjectEntityDelete2
            );
    
}