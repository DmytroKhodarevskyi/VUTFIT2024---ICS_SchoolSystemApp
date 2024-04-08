using DAL.Entities;

namespace SchoolSystem.BL.Models;

public record EvaluationListModel(): baseModel
{
    public required int Score { get; set; }
    public string? Description { get; set; }
    public Guid? StudentId { get; set; }
    public Guid? ActivityId { get; set; }
    
    public static EvaluationListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Score = 0,
        Description = string.Empty,
        StudentId = Guid.Empty,
        ActivityId = Guid.Empty
    };
}