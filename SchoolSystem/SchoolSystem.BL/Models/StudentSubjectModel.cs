namespace SchoolSystem.BL.Models;

public record StudentSubjectModel : baseModel
{
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
    
    public static StudentSubjectModel Empty => new()
    {
        Id = Guid.NewGuid(),
        StudentId = Guid.Empty,
        SubjectId = Guid.Empty
    };
}