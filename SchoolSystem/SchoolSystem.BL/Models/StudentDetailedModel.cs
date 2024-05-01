    using System.Collections.ObjectModel;
using DAL.Entities;


namespace SchoolSystem.BL.Models
{
    public record StudentDetailedModel() : baseModel
    {
        public required string Name { get; set; } 
        public required string Surname { get; set; } 
        public string? Photo { get; set; }
        
        public ObservableCollection<StudentSubjectModel> StudentSubjects { get; set; } = new();
        
        public ObservableCollection<SubjectListModel> Subjects { get; set; } = new();
        
        public static StudentDetailedModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Surname = string.Empty,

            Photo = null,
            StudentSubjects = new ObservableCollection<StudentSubjectModel>(),
            Subjects = new ObservableCollection<SubjectListModel>()

        };
    }
}