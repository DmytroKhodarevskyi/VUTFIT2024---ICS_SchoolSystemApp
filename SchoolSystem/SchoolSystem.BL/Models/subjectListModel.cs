using DAL.Entities;
using AutoMapper;

namespace SchoolSystem.BL.Models
{
    public record subjectListModel(string Name, string Abbreviation) : baseModel
    {
        public string Name { get; set; } = Name;
        public string Abbreviation { get; set; } = Abbreviation;
        
        
        public class Mapper : Profile
        {
            public Mapper()
            {
                CreateMap<SubjectEntity, subjectListModel>();

                CreateMap<StudentSubjectEntity, subjectListModel>()
                    .ConstructUsing(src => new subjectListModel("", ""))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject!.Name))
                    .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Subject!.Abbreviation));
            }
        }
        
        public static subjectListModel Empty => new(string.Empty, string.Empty);
    }
}
