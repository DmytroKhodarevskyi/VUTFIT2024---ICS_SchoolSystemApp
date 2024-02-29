using System.Diagnostics;

namespace DAL.Entities;

public record StudentEntity : IEntity
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    public required string Surname { get; set; }
    
    public string? Photo { get; set; }
    public ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();

    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
    public ICollection<EvaluationEntity> Evaluations { get; set; } = new List<EvaluationEntity>();
}

