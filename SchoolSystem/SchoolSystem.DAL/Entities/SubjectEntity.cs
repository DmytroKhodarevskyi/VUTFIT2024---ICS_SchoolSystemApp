namespace DAL.Entities;

public record SubjectEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Abbreviation { get; set; }
}