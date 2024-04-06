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
    public int Tag { get; set; } = Tag;
    public Room Room { get; set; } = Room;
    public string? Description { get; set; } = Description;
    
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ActivityEntity, ActivityListModel>();
        }
    }
    
    public static ActivityListModel Empty => new( 1, default, string.Empty);
    
}