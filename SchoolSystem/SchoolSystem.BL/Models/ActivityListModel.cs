
using DAL.Entities;
using DAL.Enums;

namespace SchoolSystem.BL.Models;

public record ActivityListModel() : baseModel
{
    public  required string Name { get; set; } 
    public  required DateTime Start { get; set; }
    public  required DateTime End { get; set; } 
    public  required string? Description { get; set; } 
    public  required int Tag { get; set; }
    public  required Room Room { get; set; }
    public  required Guid? SubjectId { get; set; }
    
    public static ActivityListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Start = DateTime.MinValue,
        End = DateTime.MinValue,
        Description = string.Empty,
        Tag = 0,
        Room = Room.D105,
        SubjectId = Guid.Empty
    };
       
}