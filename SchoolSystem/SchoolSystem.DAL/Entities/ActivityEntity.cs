namespace DAL.Entities;

public record ActivityEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public required DateTime Start { get; set; }
    public required string End { get; set; }
    public required Room Room { get; set; }
    public required int Tag { get; set; }
    public string? Description { get; set; }
    

}