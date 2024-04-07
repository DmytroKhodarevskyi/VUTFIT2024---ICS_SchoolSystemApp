using DAL.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity Activity1 = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
        Name =  "IZP",
        Start = new DateTime(2022, 1, 1, 8, 0, 0),
        End = new DateTime(2022, 1, 1, 9, 0, 0),
        Room = Room.D104,
        Tag = 1,
        Description = "Lecture",
    };
    
    public static readonly ActivityEntity Activity2 = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
        Name =  "IUS",
        Start = new DateTime(2022, 1, 1, 9, 0, 0),
        End = new DateTime(2022, 1, 1, 10, 0, 0),
        Room = Room.D105,
        Tag = 2,
        Description = "Lecture",
    };
    
    static ActivitySeeds()
    {
        Activity1.Subject = SubjectSeeds.IZP;
        Activity2.Subject = SubjectSeeds.IUS;
        Activity1.Evaluations.Add(EvaluationSeeds.Evaluation1);
        Activity2.Evaluations.Add(EvaluationSeeds.Evaluation2);
    }
    
    public static void Seed(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ActivityEntity>().HasData(
            Activity1 with{ Subject = null, Evaluations = Array.Empty<EvaluationEntity>()}, Activity2 with{ Subject = null, Evaluations = Array.Empty<EvaluationEntity>()});
}    
