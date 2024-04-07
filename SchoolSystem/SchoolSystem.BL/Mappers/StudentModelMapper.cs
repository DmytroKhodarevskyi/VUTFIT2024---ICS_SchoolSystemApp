using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Mappers;

public class StudentModelMapper : ModelMapperBase<StudentEntity, StudentListModel, StudentDetailedModel>
{
    public override StudentListModel MapToListModel(StudentEntity? entity) => entity is null
        ? StudentListModel.Empty
        : new StudentListModel()
        {
            Id = entity.Id, 
            Name = entity.Name,
            Surname = entity.Surname,
        };

    public override StudentDetailedModel MapToDetailModel(StudentEntity? entity) =>
        entity is null
            ? StudentDetailedModel.Empty
            : new StudentDetailedModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                Photo = entity.Photo
            };

    public override StudentEntity MapToEntity(StudentDetailedModel model) =>
        new() { Id = model.Id, Name = model.Name, Surname = model.Surname, Photo = model.Photo };
}