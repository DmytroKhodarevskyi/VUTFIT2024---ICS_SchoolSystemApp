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
                CreateMap<SubjectEntity, SubjectListModel>().ReverseMap();

                 }
        }
        
        public static SubjectListModel Empty => new(string.Empty, string.Empty);
    }
}
