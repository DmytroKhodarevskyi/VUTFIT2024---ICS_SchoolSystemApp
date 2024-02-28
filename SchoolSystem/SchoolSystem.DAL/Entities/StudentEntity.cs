namespace DAL.Entities;

public record StudentEntity : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public required string Name { get; set; }
    public required string Surname { get; set; }
    
    public string? Photo { get; set; }
    public ICollections<SubjectsEntity> Subjects { get; set; } = new List<SubjectsEntity>();
}

