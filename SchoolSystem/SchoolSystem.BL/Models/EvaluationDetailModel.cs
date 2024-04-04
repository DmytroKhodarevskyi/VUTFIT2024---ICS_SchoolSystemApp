using AutoMapper;
using DAL.Entities;

namespace SchoolSystem.BL.Models;

public class EvaluationDetailModel(
    int Score,
    string? Description,
    StudentEntity? Student,
    ActivityEntity? Activity
    )
{
    public int Score { get; set; }
    public string Description { get; set; }
    public StudentEntity Student { get; set; }
    public ActivityEntity Activity { get; set; }
    
    class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EvaluationDetailModel, EvaluationEntity>()
                .ReverseMap()
                .ForMember(entity => entity.Student, expression => expression.Ignore());
        }
    }
    
    public static EvaluationDetailModel Empty => new(1, String.Empty, default, default);
}