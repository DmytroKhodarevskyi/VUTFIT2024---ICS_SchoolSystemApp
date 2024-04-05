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
                CreateMap<subjectListModel, SubjectEntity>().ReverseMap();
            }
        }
        
        public static subjectListModel Empty => new(string.Empty, string.Empty);
    }
}
