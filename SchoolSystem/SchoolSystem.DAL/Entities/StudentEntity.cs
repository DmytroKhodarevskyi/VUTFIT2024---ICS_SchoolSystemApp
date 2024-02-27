namespace DAL.Entities;

public record StudentEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public required string Name { get; set; }
    public required string Surname { get; set; }
    
    public string? Photo { get; set; }
}

