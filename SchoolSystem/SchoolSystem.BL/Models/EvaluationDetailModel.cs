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
    public StudentDetailedModel? Student { get; set; }
    public ActivityDetailModel? Activity { get; set; }

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationDetailModel>()
                .ReverseMap()
                .ForMember(entity => entity.Student, expression => expression.Ignore())
                .ForMember(entity => entity.Activity, expression => expression.Ignore());
        }
    }

    public static EvaluationDetailModel Empty => new(1, String.Empty);
}