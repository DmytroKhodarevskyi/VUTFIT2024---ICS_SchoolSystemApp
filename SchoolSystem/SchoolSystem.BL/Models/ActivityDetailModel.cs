using AutoMapper;
using DAL.Entities;
using DAL.Enums;

namespace SchoolSystem.BL.Models;

public record ActivityDetailModel(
    DateTime Start,
    DateTime End,
    Room Room,
    int Tag,
    string Description
    ) : baseModel
{
    public DateTime Start { get; set; } = Start;
    public DateTime End { get; set; } = End;
    public Room Room { get; set; } = Room;
    public int Tag { get; set; } = Tag;
    public string? Description { get; set; } = Description;
    // public ICollection<EvaluationEntity>? Evaluations { get; set; } = new List<EvaluationEntity>();
    //
    public SubjectEntity? Subject { get; set; } 
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ActivityEntity, ActivityDetailModel>()
                .ReverseMap()
                .ForMember(entity => entity.Subject, expression => expression.Ignore());
        }
    }
    
    public static ActivityDetailModel Empty => new(DateTime.Now, DateTime.Now,  default, 1, String.Empty);

    
}