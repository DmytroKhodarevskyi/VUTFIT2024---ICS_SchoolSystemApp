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
                    .ForMember(dest => dest.Subjects, opt => opt.Ignore())
                    .ReverseMap();
            }
        }
        
        public static StudentDetailedModel Empty => new(string.Empty, string.Empty, null);
    }
}