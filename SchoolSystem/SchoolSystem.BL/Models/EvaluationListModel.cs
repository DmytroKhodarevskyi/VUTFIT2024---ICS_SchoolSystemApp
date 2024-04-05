using AutoMapper;
using DAL.Entities;

namespace SchoolSystem.BL.Models;

public record EvaluationListModel(int Score)
{
    public int Score { get; set; }
    
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationListModel>()
                .ReverseMap()
                .ForMember(entity => entity.Student, expression => expression.Ignore())
                .ForMember(entity => entity.Activity, expression => expression.Ignore());
        }
    }
    
    public static EvaluationListModel Empty => new(1);
}