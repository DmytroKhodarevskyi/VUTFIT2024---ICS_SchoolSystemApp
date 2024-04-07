using AutoMapper;
using DAL.Entities;

namespace SchoolSystem.BL.Models
{
    public record SubjectDetailedModel(string Name, string Abbreviation) : baseModel
    {
        public string Name { get; set; } = Name;
        public string Abbreviation { get; set; } = Abbreviation;
        public List<StudentListModel> Students { get; init; } = new();
        public List<ActivityListModel> Activities { get; init; } = new();
        
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<SubjectEntity, SubjectDetailedModel>()
                    .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.StudentSubjects))
                    .ForMember(dest => dest.Activities, opt => opt.Ignore());

                
                CreateMap<SubjectDetailedModel, SubjectEntity>()
                    .ForMember(dest => dest.StudentSubjects, opt => opt.MapFrom(src => src.Students))
                    .ForMember(dest => dest.Activities, opt => opt.Ignore());
            }
        }
        
        public static SubjectDetailedModel Empty => new(string.Empty, string.Empty);
    }
}