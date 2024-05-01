namespace SchoolSystem.BL.Models;

public record StudentSubjectModel : baseModel
{
    public Guid StudentId { get; set; }
    public Guid SubjectId { get; set; }
    
    public StudentSubjectModel(Guid studentId, Guid subjectId)
    {
        StudentId = studentId;
        SubjectId = subjectId;
    }
}