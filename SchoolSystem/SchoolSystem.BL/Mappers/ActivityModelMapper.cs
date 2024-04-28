using System.Collections.ObjectModel;
using SchoolSystem.BL.Models;
using DAL.Entities;

namespace SchoolSystem.BL.Mappers;

public class ActivityModelMapper : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    public override ActivityListModel MapToListModel(ActivityEntity? entity) => entity is null
        ? ActivityListModel.Empty
        : new ActivityListModel
        { 
            Id = entity.Id,
            Name = entity.Name,
            Start = entity.Start,
            End = entity.End,
            Description = entity.Description,
            Tag = entity.Tag,
            Room = entity.Room,
            SubjectId = entity.SubjectId
        };  

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity) =>
        entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Start = entity.Start,
                End = entity.End,
                Description = entity.Description,
                Room = entity.Room,
                SubjectId = entity.SubjectId,
                Evaluations = entity.Evaluations is null
                    ? new ObservableCollection<EvaluationListModel>()
                    : new ObservableCollection<EvaluationListModel>(entity.Evaluations.Select(e => new EvaluationModelMapper().MapToListModel(e))),
                Subject = entity.Subject is null ? SubjectListModel.Empty : new SubjectModelMapper().MapToListModel(entity.Subject)
            
            };
    
    public override ActivityEntity MapToEntity(ActivityDetailModel model) =>
        new()
        {
            Id = model.Id,
            Name = model.Name,
            Start = model.Start,
            End = model.End,
            Description = model.Description,
            Tag = model.Tag,
            Room = model.Room,
            SubjectId = model.SubjectId
        };

}