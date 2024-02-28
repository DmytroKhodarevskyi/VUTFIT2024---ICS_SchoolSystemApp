using System.Diagnostics;

namespace DAL.Entities;

public record StudentEntity : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public required string Name { get; set; }
    public required string Surname { get; set; }
    
    public string? Photo { get; set; }
    public ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();

    public virtual ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
    public virtual ICollection<EvaluationEntity> Evaluations { get; set; } = new List<EvaluationEntity>();
}

