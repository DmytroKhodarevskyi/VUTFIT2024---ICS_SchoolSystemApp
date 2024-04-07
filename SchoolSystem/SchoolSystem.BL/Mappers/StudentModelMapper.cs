using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Mappers;

public class StudentModelMapper : ModelMapperBase<StudentEntity, StudentListModel, StudentDetailedModel>
{
    public override StudentListModel MapToListModel(StudentEntity? entity) => entity is null
        ? StudentListModel.Empty
        : new StudentListModel(entity.Name, entity.Surname) { Id = entity.Id };

    public override StudentDetailedModel MapToDetailModel(StudentEntity? entity) =>
        entity is null
            ? StudentDetailedModel.Empty
            : new StudentDetailedModel(entity.Name, entity.Surname, entity.Photo) { Id = entity.Id };

    public override StudentEntity MapToEntity(StudentDetailedModel model) =>
        new() { Id = model.Id, Name = model.Name, Surname = model.Surname, Photo = model.Photo };
}