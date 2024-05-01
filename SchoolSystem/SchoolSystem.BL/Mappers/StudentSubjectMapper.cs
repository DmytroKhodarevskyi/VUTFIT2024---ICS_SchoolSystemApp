using DAL.Entities;
using DAL.Mappers;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Mappers;

public class StudentSubjectMapper : ModelMapperBase<StudentSubjectEntity, StudentSubjectModel, StudentSubjectModel>
{
    public override StudentSubjectModel MapToListModel(StudentSubjectEntity? entity) => entity is null
        ? StudentSubjectModel.Empty
        : new StudentSubjectModel()
        {
            Id = entity.Id, StudentId = entity.StudentId, SubjectId = entity.SubjectId,
            SubjectName = entity.Subject?.Name ?? string.Empty,
            StudentName = entity.Student?.Name ?? string.Empty
        };
    
    public override StudentSubjectModel MapToDetailModel(StudentSubjectEntity? entity) =>
        entity is null
            ? StudentSubjectModel.Empty
            : new StudentSubjectModel()
            {
                Id = entity.Id, StudentId = entity.StudentId, SubjectId = entity.SubjectId,
                SubjectName = entity.Subject?.Name ?? string.Empty,
                StudentName = entity.Student?.Name ?? string.Empty
            };

    public override StudentSubjectEntity MapToEntity(StudentSubjectModel model) =>
        new() { Id = model.Id, StudentId = model.StudentId, SubjectId = model.SubjectId, Student = null, Subject = null };
}