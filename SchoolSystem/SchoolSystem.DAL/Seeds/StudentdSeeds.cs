using DAL.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds;

public static class StudentSeeds
{
    public static readonly StudentEntity Student1 = new()
    {
        Id = Guid.Parse( "00000000-0000-0000-3050-000000000001"),
        Name =  "John",
        Surname = "Doe",
        Photo = "https://www.google.com",
    };

    static StudentSeeds()
    {
        Student1.Subjects.Add(SubjectSeeds.IZP);
        Student1.Subjects.Add(SubjectSeeds.IUS);
        Student1.Activities.Add(ActivitySeeds.Activity1);
        Student1.Activities.Add(ActivitySeeds.Activity2);
        Student1.Evaluations.Add(EvaluationSeeds.Evaluation1);
        Student1.Evaluations.Add(EvaluationSeeds.Evaluation2);
    }
    
    public static void Seed(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<StudentEntity>().HasData(
            Student1 with{Subjects = Array.Empty<SubjectEntity>(),
                Activities = Array.Empty<ActivityEntity>(), 
                Evaluations = Array.Empty<EvaluationEntity>()});
}