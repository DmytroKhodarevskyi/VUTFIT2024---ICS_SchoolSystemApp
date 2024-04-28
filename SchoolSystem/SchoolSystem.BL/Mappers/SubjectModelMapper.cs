using System.Collections.ObjectModel;
using SchoolSystem.BL.Models;
using DAL.Entities;



namespace SchoolSystem.BL.Mappers{

    public class SubjectModelMapper() : ModelMapperBase<SubjectEntity, SubjectListModel, SubjectDetailedModel>
    {
        public override SubjectListModel MapToListModel(SubjectEntity? entity)
            => entity is null
                ? SubjectListModel.Empty
                : new SubjectListModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Abbreviation = entity.Abbreviation
                };


        public override SubjectDetailedModel MapToDetailModel(SubjectEntity? entity)
            => entity is null
                ? SubjectDetailedModel.Empty
                : new SubjectDetailedModel()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Abbreviation = entity.Abbreviation,
                    Students =  entity.Students is null
                        ? new ObservableCollection<StudentListModel>()
                        : new ObservableCollection<StudentListModel>(entity.Students.Select(s => new StudentModelMapper(new SubjectModelMapper()).MapToListModel(s))),
                    Activities = entity.Activities is null 
                        ? new ObservableCollection<ActivityListModel>()
                        : new ObservableCollection<ActivityListModel>(entity.Activities.Select(a => new ActivityModelMapper().MapToListModel(a)))   
                    
                };


        public override SubjectEntity MapToEntity(SubjectDetailedModel model)
            => new() { Id = model.Id, Name = model.Name, Abbreviation = model.Abbreviation };
    }
}

    