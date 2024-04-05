using AutoMapper;
using DAL.Entities;


namespace SchoolSystem.BL.Models
{
    public record studentListModel(string Name, string Surname) : baseModel
    {
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        
        
        public class Mapper : Profile
        {
            public Mapper()
            {
                CreateMap<StudentEntity, studentListModel>().ReverseMap();
            }
        }       
        public static studentListModel Empty => new(string.Empty, string.Empty);
    }
}