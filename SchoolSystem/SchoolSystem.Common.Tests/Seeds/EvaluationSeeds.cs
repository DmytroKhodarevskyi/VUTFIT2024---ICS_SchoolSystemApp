using DAL.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Common.Tests.Seeds;

public static class EvaluationSeeds
{
    public static readonly EvaluationEntity EmptyEvaluationEntity = new()
    {
        Id = default,
        Score = default,
        Description = default!,
    };
    
    public static readonly EvaluationEntity EvaluationWithNothing = new()
    {
        Id = Guid.Parse("0d4fa150-ad80-4d46-a511-4c888166e112"),
        Score = 2,
        Description = "Test",
    };
    
    public static readonly EvaluationEntity Evaluation1 = new()
    {
        Id = Guid.Parse("0d4fa150-ad80-4d46-a511-4c666166e112"),
        Score = 2,
        Description = "Test",
    };
    
    public static readonly EvaluationEntity Evaluation2 = new()
    {
        Id = Guid.Parse("0d4fa150-ad80-4d46-a511-4c666122e12e"),
        Score = 3,
        Description = "Test",
    };
    
    public static readonly EvaluationEntity EvaluationEntityWithNoStudAct = Evaluation1 with 
        { 
            Id = Guid.Parse("0d4fa150-ad80-4d46-a511-4c666111e12e"), 
            Student = null,
            Activity = null };
    
    public static readonly EvaluationEntity EvaluationEntityUpdated1 = Evaluation1 with { Id = Guid.Parse("00000000-0000-1233-0000-000000000004"),
        Student = null,
        Activity = null };
    
    public static readonly EvaluationEntity EvaluationEntityDeleted1 = Evaluation1 with { Id = Guid.Parse("00000000-0000-0000-4231-000000000005"),
        Student = null,
        Activity = null };
    
    public static readonly EvaluationEntity EvaluationEntityUpdate2 = Evaluation2 with { Id = Guid.Parse("0d4fa150-3123-4d46-a511-4c666166e12e"),
        Student = null,
        Activity = null };
    
    public static readonly EvaluationEntity EvaluationEntityDelete2 = Evaluation2 with { Id = Guid.Parse("0d4fa150-1234-4d46-a511-4c666166e12e"),
        Student = null,
        Activity = null };
    
    // static EvaluationSeeds()
    // {
    //     // Evaluation1.Student = StudentSeeds.Student1;
    //     Evaluation2.Student = StudentSeeds.Student1;
    //     // Evaluation1.Activity = ActivitySeeds.Activity1;
    //     Evaluation2.Activity = ActivitySeeds.Activity2;
    // }
    
    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<EvaluationEntity>().HasData(
            Evaluation1 with {Student = null, Activity = null}, Evaluation2 with {Student = null, Activity = null},
            EvaluationEntityWithNoStudAct,
            EvaluationEntityUpdated1,
            EvaluationEntityDeleted1,
            EvaluationEntityUpdate2,
            EvaluationEntityDelete2
            );
}