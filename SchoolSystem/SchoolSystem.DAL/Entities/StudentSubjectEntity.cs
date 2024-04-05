namespace DAL.Entities;

public record StudentSubjectEntity : IEntity
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
    public StudentEntity? Student { get; set; }
    public SubjectEntity? Subject { get; set; }
}