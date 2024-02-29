using DAL.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Common.Tests.Seeds;

public static class EvaluationSeeds
{
    public static readonly EvaluationEntity EmptyEvaluation = new()
    {
        Id = default,
        Score = default,
        Description = default!,
    };
    
    public static readonly EvaluationEntity Evaluation1 = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        Score = 2,
        Description = "Test",
    };
    
    public static readonly EvaluationEntity Evaluation2 = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
        Score = 3,
        Description = "Test",
    };
    
    public static readonly EvaluationEntity EvaluationEntityWithNoStudAct = Evaluation1 with 
        { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), 
            Student = null,
            Activity = null };
    
    public static readonly EvaluationEntity EvaluationEntityUpdated1 = Evaluation1 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
        Student = null,
        Activity = null };
    
    public static readonly EvaluationEntity EvaluationEntityDeleted1 = Evaluation1 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
        Student = null,
        Activity = null };
    
    public static readonly EvaluationEntity EvaluationEntityUpdate2 = Evaluation2 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
        Student = null,
        Activity = null };
    
    public static readonly EvaluationEntity EvaluationEntityDelete2 = Evaluation2 with { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
        Student = null,
        Activity = null };
    
    static EvaluationSeeds()
    {
        Evaluation1.Student = StudentSeeds.Student1;
        Evaluation2.Student = StudentSeeds.Student1;
        Evaluation1.Activity = ActivitySeeds.Activity1;
        Evaluation2.Activity = ActivitySeeds.Activity2;
    }
    
    public static void Seed(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<EvaluationEntity>().HasData(
            Evaluation1, Evaluation2 with {Student = null, Activity = null},
            EvaluationEntityWithNoStudAct,
            EvaluationEntityUpdated1,
            EvaluationEntityDeleted1,
            EvaluationEntityUpdate2,
            EvaluationEntityDelete2
            );
}    
