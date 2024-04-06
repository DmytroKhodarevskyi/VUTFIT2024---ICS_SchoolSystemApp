using DAL.Entities;
using AutoMapper;

namespace SchoolSystem.BL.Models
{
    public record SubjectListModel(string Name, string Abbreviation) : baseModel
    {
        public string Name { get; set; } = Name;
        public string Abbreviation { get; set; } = Abbreviation;
        
        
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<SubjectEntity, SubjectListModel>();

                CreateMap<StudentSubjectEntity, SubjectListModel>()
                    .ConstructUsing(src => new SubjectListModel("", ""))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject!.Name))
                    .ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.Subject!.Abbreviation));
            }
        }
        
        public static SubjectListModel Empty => new(string.Empty, string.Empty);
    }
}
