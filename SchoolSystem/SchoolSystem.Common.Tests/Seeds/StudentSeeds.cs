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
        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        Name = "John",
        Surname = "Doe",
        Photo = "https://www.google.com",
    };
    
    public static readonly StudentEntity StudentEntityWithNoSubjActivEval = Student1 with 
        { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), 
            Subjects = Array.Empty<SubjectEntity>(), 
            Activities = Array.Empty<ActivityEntity>(),
            Evaluations = Array.Empty<EvaluationEntity>() };
    public static readonly StudentEntity StudentEntityUpdated = Student1 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
        Subjects = Array.Empty<SubjectEntity>(), 
        Activities = Array.Empty<ActivityEntity>(),
        Evaluations = Array.Empty<EvaluationEntity>() };
    public static readonly StudentEntity StudentEntityDeleted = Student1 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
        Subjects = Array.Empty<SubjectEntity>(), 
        Activities = Array.Empty<ActivityEntity>(),
        Evaluations = Array.Empty<EvaluationEntity>() };
    
    public static readonly StudentEntity StudentSubjectEntityUpdate = Student1 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
        Subjects = Array.Empty<SubjectEntity>(), 
        Activities = Array.Empty<ActivityEntity>(),
        Evaluations = Array.Empty<EvaluationEntity>() };
    public static readonly StudentEntity StudentSubjectEntityDelete = Student1 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000055"),
        Subjects = Array.Empty<SubjectEntity>(), 
        Activities = Array.Empty<ActivityEntity>(),
        Evaluations = Array.Empty<EvaluationEntity>() };
    
    public static readonly StudentEntity StudentActivityEntityUpdate = Student1 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
        Subjects = Array.Empty<SubjectEntity>(), 
        Activities = Array.Empty<ActivityEntity>(),
        Evaluations = Array.Empty<EvaluationEntity>() };
    public static readonly StudentEntity StudentActivityEntityDelete = Student1 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
        Subjects = Array.Empty<SubjectEntity>(), 
        Activities = Array.Empty<ActivityEntity>(),
        Evaluations = Array.Empty<EvaluationEntity>() };
    
    public static readonly StudentEntity StudentEvaluationEntityUpdate = Student1 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
        Subjects = Array.Empty<SubjectEntity>(), 
        Activities = Array.Empty<ActivityEntity>(),
        Evaluations = Array.Empty<EvaluationEntity>() };
    public static readonly StudentEntity StudentEvaluationEntityDelete = Student1 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
        Subjects = Array.Empty<SubjectEntity>(), 
        Activities = Array.Empty<ActivityEntity>(),
        Evaluations = Array.Empty<EvaluationEntity>() };

    static StudentSeeds()
    {
        Student1.Subjects.Add(SubjectSeeds.IZP);
        Student1.Subjects.Add(SubjectSeeds.IUS);
        
        Student1.Activities.Add(ActivitySeeds.Activity1);
        Student1.Activities.Add(ActivitySeeds.Activity2);
        
        Student1.Evaluations.Add(EvaluationSeeds.Evaluation1);
        Student1.Evaluations.Add(EvaluationSeeds.Evaluation2);
    }
    
    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<StudentEntity>().HasData(
            Student1 with {Subjects = Array.Empty<SubjectEntity>(),
                Activities = Array.Empty<ActivityEntity>(), 
                Evaluations = Array.Empty<EvaluationEntity>()},
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