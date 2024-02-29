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
        Score = 2,
        Description = "Second evaluation",
    };
    
    static EvaluationSeeds()
    {
        Evaluation1.Activity = ActivitySeeds.Activity1;
        Evaluation2.Activity = ActivitySeeds.Activity2;
    }
    
    public static void Seed(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<EvaluationEntity>().HasData(
            Evaluation1 with{ Activity = null,
                Student = null} , Evaluation2 with{ Activity = null,
                Student = null});
}