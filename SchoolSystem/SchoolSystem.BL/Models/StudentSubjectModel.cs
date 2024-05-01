namespace SchoolSystem.BL.Models;

public record StudentSubjectModel : baseModel
{
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
    
    public string? StudentName { get; set; }
    
    public string? SubjectName { get; set; }
    
    public static StudentSubjectModel Empty => new()
    {
        Id = Guid.NewGuid(),
        StudentId = Guid.Empty,
        SubjectId = Guid.Empty,
        StudentName = string.Empty,
        SubjectName = string.Empty
    };
}