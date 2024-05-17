
using System.Collections.ObjectModel;
using DAL.Entities;
using DAL.Enums;

namespace SchoolSystem.BL.Models;

public record ActivityDetailModel() : baseModel
{
    public required string Name { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; } 
    public Room Room { get; set; }  
    public int Tag { get; set; } 
    public string? Description { get; set; } 
    public ICollection<EvaluationListModel>? Evaluations { get; set; } = new List<EvaluationListModel>();
    
    public Guid? SubjectId { get; set; }

    public SubjectListModel? Subject { get; set; }
    
    public string SubjectAbr { get; set; }

    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Start = DateTime.Today,
        End = DateTime.Today.AddDays(1),
        Room = Room.D105,
        Tag = 1,
        Description = string.Empty,
        SubjectId = default,
        Evaluations = new ObservableCollection<EvaluationListModel>(),
        Subject = SubjectListModel.Empty,
        SubjectAbr = string.Empty
    };

    
}