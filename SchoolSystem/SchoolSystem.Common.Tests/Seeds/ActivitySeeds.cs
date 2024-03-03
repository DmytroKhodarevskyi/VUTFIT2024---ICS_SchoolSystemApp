using DAL.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Common.Tests.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity EmptyActivity = new()
    {
           Id = default,
            Start = default,
            End = default,
            Room = default,
            Tag = default,
            Description = default!, 
    };
    
public static readonly ActivityEntity Activity1 = new()
    {
        Id = Guid.Parse("00000000-0000-aaaa-0000-000000000001"),
        Start = DateTime.Now,
        End = DateTime.Now,
        Room = Room.D105,
        Tag = 1,
        Description = "Test",
    };

    public static readonly ActivityEntity Activity2 = new()
    {
        Id = Guid.Parse("00000000-0000-12cc-0000-000000000002"),
        Start = DateTime.Now,
        End = DateTime.Now,
        Room = Room.D104,
        Tag = 2,
        Description = "Test",
    };
    
    public static readonly ActivityEntity ActivityEntityWithNoSubjEval = Activity1 with 
        { Id = Guid.Parse("00000000-0000-0000-8ccc-000000000003"), 
            Subject = null, 
            Evaluations = Array.Empty<EvaluationEntity>() };
    
    public static readonly ActivityEntity ActivityEntityUpdated = Activity1 with { Id = Guid.Parse("00000000-56cc-0000-0000-000000000004"), 
        Subject = null, 
        Evaluations = Array.Empty<EvaluationEntity>() };
    public static readonly ActivityEntity ActivityEntityDeleted = Activity1 with { Id = Guid.Parse("00000000-5aaa-0000-0000-000000000005"), 
        Subject = null,
        Evaluations = Array.Empty<EvaluationEntity>() };
    public static readonly ActivityEntity ActivityEntityUpdate2 = Activity2 with { Id = Guid.Parse("00000000-0000-0000-99aa-000000000006"),
        Subject = null,
        Evaluations = Array.Empty<EvaluationEntity>() };
    public static readonly ActivityEntity ActivityEntityDelete2 = Activity2 with { Id = Guid.Parse("00000000-0000-0000-aa55-000000000007"),
        Subject = null,
        Evaluations = Array.Empty<EvaluationEntity>() };

    static ActivitySeeds()
    {
        // Activity1.Subject = SubjectSeeds.IZP;
        // Activity2.Subject = SubjectSeeds.IUS;
        Activity1.Evaluations.Add(EvaluationSeeds.Evaluation1);
        Activity2.Evaluations.Add(EvaluationSeeds.Evaluation2);
    }
    
    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ActivityEntity>().HasData(
            Activity1  with{ Subject = null, Evaluations = Array.Empty<EvaluationEntity>()}, Activity2 with{ Subject = null, Evaluations = Array.Empty<EvaluationEntity>()},
            ActivityEntityWithNoSubjEval,
            ActivityEntityUpdated,
            ActivityEntityDeleted,
            ActivityEntityUpdate2,
            ActivityEntityDelete2
            );
}