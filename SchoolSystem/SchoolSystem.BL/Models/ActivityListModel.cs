using AutoMapper;
using DAL.Entities;
using DAL.Enums;

namespace SchoolSystem.BL.Models;

public record ActivityListModel(
    DateTime Start,
    DateTime End,
    int Tag,
    Room Room
    ) : baseModel
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Tag { get; set; }
    public Room Room { get; set; }
    
    class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ActivityListModel, ActivityEntity>()
                .ReverseMap();
        }
    }
    
    public static ActivityListModel Empty => new(DateTime.Now, DateTime.Now, 1, default);
    
}