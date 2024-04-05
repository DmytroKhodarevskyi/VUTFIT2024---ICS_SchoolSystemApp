using AutoMapper;
using DAL.Entities;
using DAL.Enums;

namespace SchoolSystem.BL.Models;

public record ActivityListModel(
    int Tag,
    Room Room,
    string Description
    ) : baseModel
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Tag { get; set; } = Tag;
    public Room Room { get; set; } = Room;
    public string Description { get; set; } = Description;
    
    class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ActivityEntity, ActivityListModel>()
                .ForMember(entity => entity.Start, expression => expression.Ignore())
                .ForMember(entity => entity.End, expression => expression.Ignore())
                .ReverseMap();
        }
    }
    
    public static ActivityListModel Empty => new( 1, default, string.Empty);
    
}