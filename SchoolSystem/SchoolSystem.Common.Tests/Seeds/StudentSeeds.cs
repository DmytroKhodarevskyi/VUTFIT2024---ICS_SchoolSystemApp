using DAL.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Common.Tests.Seeds;

public static class StudentSeeds
{
    public static readonly StudentEntity EmptyStudentEntity = new()
    {
        Id = default,
        Name = default!,
        Surname = default!,
        Photo = string.Empty,
    };

    public static readonly StudentEntity Student1 = new()
    {
        Id = Guid.Parse("0dafa151-ac90-4d46-a513-4c666166e12e"),
        Name = "John",
        Surname = "Doe",
        Photo = "https://www.google.com",
    };
    public static readonly StudentEntity Student2 = new()
    {
        Id = Guid.Parse("0dafa151-ac12-4d46-a513-4c666166e12e"),
        Name = "Dima",
        Surname = "Trifanov",
        Photo = "https://www.mellstryit.com",
    };
    public static readonly StudentEntity Student3 = new()
    {
        Id = Guid.Parse("0dafa151-ac93-4d46-a513-4c666166e12e"),
        Name = "Dima",
        Surname = "Khodarevsky",
        Photo = "https://www.mellstryit.com",
    };

    public static readonly StudentEntity StudentEntityWithNoSubjActivEval = Student1 with 
        { Id = Guid.Parse("00000000-0000-0000-0ac6-000000000002"), 
            StudentSubjects = Array.Empty<StudentSubjectEntity>(),
            Subjects = Array.Empty<SubjectEntity>(),
        };
    public static readonly StudentEntity StudentEntityUpdated = Student1 with { Id = Guid.Parse("00000000-0000-2346-0000-000000000003"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>(),
        Subjects = Array.Empty<SubjectEntity>(),};

    public static readonly StudentEntity StudentEntityDeleted = Student1 with { Id = Guid.Parse("00000000-0000-0000-7345-000000000004"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>() ,
        Subjects = Array.Empty<SubjectEntity>(),};
    
    public static readonly StudentEntity StudentSubjectEntityUpdate = Student1 with { Id = Guid.Parse("000000aa-0000-0000-0000-000000000005"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>(),
        Subjects = Array.Empty<SubjectEntity>(),};
    public static readonly StudentEntity StudentSubjectEntityDelete = Student1 with { Id = Guid.Parse("0000a000-0000-0000-0000-000000000055"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>(),
        Subjects = Array.Empty<SubjectEntity>(),};
    
    public static readonly StudentEntity StudentActivityEntityUpdate = Student1 with { Id = Guid.Parse("00000000-1557-0000-0000-000000000006"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>() ,
        Subjects = Array.Empty<SubjectEntity>(),};
    public static readonly StudentEntity StudentActivityEntityDelete = Student1 with { Id = Guid.Parse("00000000-1488-0000-0000-000000000007"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>(),
        Subjects = Array.Empty<SubjectEntity>(),};
    
    public static readonly StudentEntity StudentEvaluationEntityUpdate = Student1 with { Id = Guid.Parse("00000000-8888-0000-0000-000000000008"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>(),
        Subjects = Array.Empty<SubjectEntity>(),};
    public static readonly StudentEntity StudentEvaluationEntityDelete = Student1 with { Id = Guid.Parse("00000000-5555-0000-0000-000000000009"),
        StudentSubjects = Array.Empty<StudentSubjectEntity>(),
        Subjects = Array.Empty<SubjectEntity>(),};
static StudentSeeds()
{
    Student1.Subjects!.Add(SubjectSeeds.IZP);
    Student1.Subjects.Add(SubjectSeeds.IUS);
}
    
public static void Seed(this ModelBuilder modelBuilder) =>
    modelBuilder.Entity<StudentEntity>().HasData(
        Student1 with {StudentSubjects = Array.Empty<StudentSubjectEntity>(), Subjects = Array.Empty<SubjectEntity>()},
        Student2 with {StudentSubjects = Array.Empty<StudentSubjectEntity>(), Subjects = Array.Empty<SubjectEntity>()},
        Student3 with {StudentSubjects = Array.Empty<StudentSubjectEntity>(), Subjects = Array.Empty<SubjectEntity>()},
        StudentEntityWithNoSubjActivEval,
        StudentEntityUpdated,
        StudentEntityDeleted,
        StudentSubjectEntityUpdate,
        StudentSubjectEntityDelete,
        StudentActivityEntityUpdate,
        StudentActivityEntityDelete,
        StudentEvaluationEntityUpdate,
        StudentEvaluationEntityDelete
    );
}