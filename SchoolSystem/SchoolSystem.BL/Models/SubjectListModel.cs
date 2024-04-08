using DAL.Entities;

namespace SchoolSystem.BL.Models
{
    public record SubjectListModel() : baseModel
    {
        public string Name { get; set; } 
        public string Abbreviation { get; set; } 
        
        public static SubjectListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Abbreviation = string.Empty
        };
    }
}
