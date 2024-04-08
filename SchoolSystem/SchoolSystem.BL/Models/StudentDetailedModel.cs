using DAL.Entities;


namespace SchoolSystem.BL.Models
{
    public record StudentDetailedModel() : baseModel
    {
        public required string Name { get; set; } 
        public required string Surname { get; set; } 
        public string? Photo { get; set; }
        
        //public List<SubjectListModel> Subjects { get; set; } = new();
        
        public static StudentDetailedModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Surname = string.Empty,
            Photo = string.Empty
        };
    }
}