using AutoMapper;
using DAL.Entities;

namespace SchoolSystem.BL.Models;

public record EvaluationDetailModel(
    int Score,
    string? Description
) : baseModel
{
    public int Score { get; set; } = Score;
    public string? Description { get; set; } = Description;
    public Guid? ActivityId { get; set; }
    public StudentDetailedModel? Student { get; set; }
    public Guid? StudentId { get; set; }
    public ActivityDetailModel? Activity { get; set; }

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationDetailModel>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.Activity, opt => opt.MapFrom(src => src.ActivityId))
                .ReverseMap();
            
        }
    }

    public static EvaluationDetailModel Empty => new(1, String.Empty);
}