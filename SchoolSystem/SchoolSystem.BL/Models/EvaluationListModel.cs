using AutoMapper;
using DAL.Entities;

namespace SchoolSystem.BL.Models;

public class EvaluationListModel(int Score)
{
    public int Score { get; set; } = Score;
    public string Description { get; set; }
    public StudentEntity Student { get; set; }
    public ActivityEntity Activity { get; set; }
    
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationListModel>()
                .ForMember(entity => entity.Description, expression => expression.Ignore())
                .ForMember(entity => entity.Student, expression => expression.Ignore())
                .ForMember(entity => entity.Activity, expression => expression.Ignore())
                .ReverseMap();
        }
    }
    
    public static EvaluationListModel Empty => new(1);
}