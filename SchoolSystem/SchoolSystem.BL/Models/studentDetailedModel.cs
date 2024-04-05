using AutoMapper;
using DAL.Entities;


namespace SchoolSystem.BL.Models
{
    public record studentDetailedModel(string Name, string Surname, string? Photo) : baseModel
    {
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        public string? Photo { get; set; }
        
        public class Mapper : Profile
        {
            public Mapper()
            {
                CreateMap<StudentEntity, studentDetailedModel>().ReverseMap();
            }
        }
        
        public static studentDetailedModel Empty => new(string.Empty, string.Empty, null);
    }
}