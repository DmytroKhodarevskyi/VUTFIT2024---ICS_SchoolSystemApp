using System.Collections.ObjectModel;
using DAL.Entities;
using DAL.Mappers;

namespace SchoolSystem.BL.Models
{
    public record SubjectDetailedModel() : baseModel
    {
        public string Name { get; set; } 
        public string Abbreviation { get; set; } 
        public ObservableCollection<StudentSubjectModel> StudentSubjects { get; set; } = new();
        public ObservableCollection<ActivityListModel> Activities { get; set; } = new();
        
        public static SubjectDetailedModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Abbreviation = string.Empty,
            StudentSubjects = new ObservableCollection<StudentSubjectModel>(),
            Activities = new ObservableCollection<ActivityListModel>()
        };
    }
}