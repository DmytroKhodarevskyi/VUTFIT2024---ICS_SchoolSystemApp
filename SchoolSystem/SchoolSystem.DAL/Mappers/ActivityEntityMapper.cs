
// using SchoolSystem.DAL;

using DAL.Entities;

namespace DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Start = newEntity.Start;
        existingEntity.End = newEntity.End;
        existingEntity.Room = newEntity.Room;
        existingEntity.Tag = newEntity.Tag;
        existingEntity.Description = newEntity.Description;
        existingEntity.Name = newEntity.Name;
        existingEntity.SubjectId = newEntity.SubjectId;
    }
}
