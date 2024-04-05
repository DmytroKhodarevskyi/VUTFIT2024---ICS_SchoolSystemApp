using AutoMapper;
using DAL.Entities;

namespace SchoolSystem.BL.Models
{
    public record subjectDetailedModel(string Name, string Abbreviation) : baseModel
    {
        public string Name { get; set; } = Name;
        public string Abbreviation { get; set; } = Abbreviation;
        public List<studentListModel> Students { get; init; } = new();
        
        public class Mapper : Profile
        {
            public Mapper()
            {
                CreateMap<SubjectEntity, subjectDetailedModel>()
                    .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));
                // Assuming SubjectEntity has a property named Students representing the collection of students
                // Adjust this according to the actual structure of your entities
                // This assumes that studentListModel can be directly mapped from the entity
                // If additional conversion logic is required, you can provide a custom mapping for studentListModel
                // Example: .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students.Select(student => ConvertToStudentListModel(student))));
                
                CreateMap<subjectDetailedModel, SubjectEntity>().ReverseMap();
            }
        }
        
        public static subjectDetailedModel Empty => new(string.Empty, string.Empty);
    }
}