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
                    .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));
                
                CreateMap<SubjectDetailedModel, SubjectEntity>()
                    .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students
                        .Select(s => new StudentSubjectEntity() { StudentId = s.Id, SubjectId = src.Id })))
                    .ForMember(dest => dest.Activities, opt => opt.Ignore());
            }
        }
        
        public static SubjectDetailedModel Empty => new(string.Empty, string.Empty);
    }
}