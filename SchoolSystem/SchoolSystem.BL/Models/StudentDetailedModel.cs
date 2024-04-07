using AutoMapper;
using DAL.Entities;


namespace SchoolSystem.BL.Models
{
    public record StudentDetailedModel(string Name, string Surname, string? Photo) : baseModel
    {
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        public string? Photo { get; set; } = Photo;
        
        public List<SubjectListModel> Subjects { get; set; } = new();
        
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<StudentEntity, StudentDetailedModel>()
                    .ForMember(s => s.Subjects, opt => opt.MapFrom(src => src.StudentSubjects))
                    .ReverseMap()
                    .ForMember(dest => dest.StudentSubjects, opt => opt.MapFrom(src => src.Subjects));
            }
        }
        
        public static StudentDetailedModel Empty => new(string.Empty, string.Empty, null);
    }
}