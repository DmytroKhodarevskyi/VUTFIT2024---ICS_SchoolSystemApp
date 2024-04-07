using System.Diagnostics;

namespace DAL.Entities;

public record StudentEntity : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    
    public string? Photo { get; set; }
    public ICollection<StudentSubjectEntity>? StudentSubjects { get; set; } = new List<StudentSubjectEntity>();
    
}

