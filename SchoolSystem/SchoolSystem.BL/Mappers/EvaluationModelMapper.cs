using  SchoolSystem.BL.Models;
using DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class EvaluationModelMapper : ModelMapperBase<EvaluationEntity, EvaluationListModel, EvaluationDetailModel>
{
    public override EvaluationListModel MapToListModel(EvaluationEntity? entity) => entity is null
        ? EvaluationListModel.Empty
        : new EvaluationListModel()
        {
            Id = entity.Id, Score = entity.Score, Description = entity.Description,
            ActivityId = entity.ActivityId, StudentId = entity.StudentId
        };
    
    public override EvaluationDetailModel MapToDetailModel(EvaluationEntity? entity) =>
        entity is null
            ? EvaluationDetailModel.Empty
            : new EvaluationDetailModel()
            {
                Id = entity.Id, Score = entity.Score, Description = entity.Description,
                ActivityId = entity.ActivityId, StudentId = entity.StudentId,
                Activity = entity.Activity is null ? ActivityListModel.Empty : new ActivityListModel() { Id = entity.Activity.Id, Description = entity.Activity.Description, 
                    End = entity.Activity.End, Name = entity.Activity.Name, Start = entity.Activity.Start, Room = entity.Activity.Room, Tag = entity.Activity.Tag, SubjectId = null},
                Student = entity.Student is null ? StudentListModel.Empty : new StudentListModel() { Id = entity.Student.Id, Name = entity.Student.Name, Surname = entity.Student.Surname}
            };

    public override EvaluationEntity MapToEntity(EvaluationDetailModel model) =>
        new() { Id = model.Id,  Activity = null!, ActivityId = model.ActivityId, StudentId = model.StudentId, Student = null!, Score = model.Score, Description = model.Description};
}