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
    public int Tag { get; set; }
    public Room Room { get; set; }
    public string? Description { get; set; }
    
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ActivityEntity, ActivityListModel>();
        }
    }
    
    public static ActivityListModel Empty => new( 1, default, string.Empty);
    
}