
using DAL.Entities;

namespace SchoolSystem.BL.Models;

public record EvaluationDetailModel() : baseModel
{
    public required int Score { get; set; }
    public string? Description { get; set; }    
    public Guid? ActivityId { get; set; }
    public Guid? StudentId { get; set; }

    public static EvaluationDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Score = 0,
        Description = string.Empty,
        ActivityId = Guid.Empty,
        StudentId = Guid.Empty
    };
}