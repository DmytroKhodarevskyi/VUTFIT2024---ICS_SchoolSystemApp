using DAL.Entities;


namespace SchoolSystem.BL.Models
{
    public record StudentListModel() : baseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; } 
        
        public static StudentListModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Surname = string.Empty
        };
    }
}