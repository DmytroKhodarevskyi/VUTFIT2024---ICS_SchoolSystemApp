using AutoMapper;
using DAL.Entities;

namespace SchoolSystem.BL.Models;

public class EvaluationListModel(int Score)
{
    public int Score { get; set; }
    
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EvaluationEntity, EvaluationListModel>();
        }
    }
    
    public static EvaluationListModel Empty => new(1);
}