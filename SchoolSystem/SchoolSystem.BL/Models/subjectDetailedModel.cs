using AutoMapper;
using DAL.Entities;

namespace SchoolSystem.BL.Models
{
    public record subjectDetailedModel(string Name, string Abbreviation) : baseModel
    {
        public string Name { get; set; } = Name;
        public string Abbreviation { get; set; } = Abbreviation;
        public List<studentListModel> Students { get; init; } = new();
        public List<ActivityListModel> Activities { get; init; } = new();
        
        public class Mapper : Profile
        {
            public Mapper()
            {
                CreateMap<SubjectEntity, subjectDetailedModel>()
                    .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));
                
                CreateMap<subjectDetailedModel, SubjectEntity>()
                    .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students
                        .Select(s => new StudentSubjectEntity() { StudentId = s.Id, SubjectId = src.Id })))
                    .ForMember(dest => dest.Activities, opt => opt.Ignore());
            }
        }
        
        public static subjectDetailedModel Empty => new(string.Empty, string.Empty);
    }
}