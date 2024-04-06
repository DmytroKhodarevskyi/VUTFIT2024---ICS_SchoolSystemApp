using AutoMapper;
using DAL.Entities;


namespace SchoolSystem.BL.Models
{
    public record StudentListModel(string Name, string Surname) : baseModel
    {
        public string Name { get; set; } = Name;
        public string Surname { get; set; } = Surname;
        
        
        
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<StudentEntity, StudentListModel>();

            }
        }       
        public static StudentListModel Empty => new(string.Empty, string.Empty);
    }
}