namespace DAL.Entities;

public record EvaluationEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int Score { get; set; }
    public string? Description { get; set; }
}