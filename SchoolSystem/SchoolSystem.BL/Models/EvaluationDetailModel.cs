using AutoMapper;
using DAL.Entities;

namespace SchoolSystem.BL.Models;

public record EvaluationDetailModel(
    int Score,
    string? Description,
    StudentEntity? Student,
    ActivityEntity? Activity
    ): baseModel
{
    public int Score { get; set; }
    public string? Description { get; set; }
    public StudentEntity? Student { get; set; }
    public ActivityEntity? Activity { get; set; }
    
    class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationDetailModel>()
                .ReverseMap()
                .ForMember(entity => entity.Student, expression => expression.Ignore())
                .ForMember(entity => entity.Activity, expression => expression.Ignore());
        }
    }
    
    public static EvaluationDetailModel Empty => new(1, String.Empty, default, default);
}