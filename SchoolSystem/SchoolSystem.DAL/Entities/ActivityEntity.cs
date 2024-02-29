using DAL.Enums;
namespace DAL.Entities;

public record ActivityEntity : IEntity
{
    public Guid Id { get; set; } 
    
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required Room Room { get; set; }
    public required int Tag { get; set; }
    public string? Description { get; set; }
    public ICollection<EvaluationEntity>? Evaluations { get; set; } = new List<EvaluationEntity>();
    
    public SubjectEntity? Subject { get; set; }
}