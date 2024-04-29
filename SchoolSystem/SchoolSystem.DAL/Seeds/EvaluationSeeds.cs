using DAL.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds;

public static class EvaluationSeeds
{
    public static readonly EvaluationEntity Evaluation1 = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
        Score = 2,
        Description = "First evaluation",
        
    };
    
    public static readonly EvaluationEntity Evaluation2 = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
        Score = 110,
        Description = "Second evaluation",
    };
    
    static EvaluationSeeds()
    {
        Evaluation1.Activity = ActivitySeeds.Activity1;
        Evaluation2.Activity = ActivitySeeds.Activity2;
        Evaluation1.Student = StudentSeeds.Student1;
        Evaluation2.Student = StudentSeeds.Student1;
        Evaluation1.ActivityId = ActivitySeeds.Activity1.Id;
        Evaluation2.ActivityId = ActivitySeeds.Activity2.Id;
        Evaluation1.StudentId = StudentSeeds.Student1.Id;
        Evaluation2.StudentId = StudentSeeds.Student1.Id;
    }
    
    public static void Seed(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<EvaluationEntity>().HasData(
            Evaluation1 with{ Activity = null,
                Student = null} , Evaluation2 with{ Activity = null,
                Student = null});
            
}