using DAL.Entities;

namespace SchoolSystem.BL.Models
{
    public record SubjectDetailedModel() : baseModel
    {
        public string Name { get; set; } 
        public string Abbreviation { get; set; } 
        //public List<StudentListModel> Students { get; init; } = new();
        //public List<ActivityListModel> Activities { get; init; } = new();
        
        public static SubjectDetailedModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Abbreviation = string.Empty
        };
    }
}