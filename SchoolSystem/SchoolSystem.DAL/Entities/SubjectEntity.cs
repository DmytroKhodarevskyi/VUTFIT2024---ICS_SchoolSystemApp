namespace DAL.Entities;

public record SubjectEntity : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
    public ICollections<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
}