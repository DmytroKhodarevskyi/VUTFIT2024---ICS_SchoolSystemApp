using System.Collections.ObjectModel;
using DAL.Entities;

namespace SchoolSystem.BL.Models
{
    public record SubjectDetailedModel() : baseModel
    {
        public string Name { get; set; } 
        public string Abbreviation { get; set; } 
        public ObservableCollection<StudentListModel> Students { get; set; } = new();
        public ObservableCollection<ActivityListModel> Activities { get; set; } = new();
        
        public static SubjectDetailedModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Abbreviation = string.Empty,
            Students = new ObservableCollection<StudentListModel>(),
            Activities = new ObservableCollection<ActivityListModel>()
        };
    }
}