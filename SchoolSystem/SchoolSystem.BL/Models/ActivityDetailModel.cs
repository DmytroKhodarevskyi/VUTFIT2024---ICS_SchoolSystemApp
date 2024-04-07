using AutoMapper;
using DAL.Entities;
using DAL.Enums;

namespace SchoolSystem.BL.Models;

public record ActivityDetailModel(
    string Name,
    DateTime Start,
    DateTime End,
    Room Room,
    int Tag,
    string Description
    ) : baseModel
{
    public DateTime Start { get; set; } = Start;
    public DateTime End { get; set; }   = End;
    public Room Room { get; set; }   = Room;
    public int Tag { get; set; }    = Tag;
    public string? Description { get; set; } = Description;
    public ICollection<EvaluationListModel>? Evaluations { get; set; } = new List<EvaluationListModel>();
    
    public Guid? SubjectId { get; set; }
    public SubjectDetailedModel? Subject { get; set; }
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ActivityEntity, ActivityDetailModel>()
                .ReverseMap()
                .ForMember(entity => entity.Subject, expression => expression.Ignore());
        }
    }
    
    public static ActivityDetailModel Empty => new(String.Empty,DateTime.Now, DateTime.Now,  default, 1, String.Empty);

    
}