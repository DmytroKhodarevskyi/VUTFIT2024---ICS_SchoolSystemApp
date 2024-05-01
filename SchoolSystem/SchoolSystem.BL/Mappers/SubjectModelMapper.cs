using SchoolSystem.BL.Models;
using DAL.Entities;



namespace SchoolSystem.BL.Mappers{

    public class SubjectModelMapper : ModelMapperBase<SubjectEntity, SubjectListModel, SubjectDetailedModel>
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
                    Abbreviation = entity.Abbreviation
                };


        public override SubjectEntity MapToEntity(SubjectDetailedModel model)
            => new() { Id = model.Id, Name = model.Name, Abbreviation = model.Abbreviation };
    }
}