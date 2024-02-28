
using DAL.Entities;

namespace SchoolSystem.DAL.Mappers;

public interface IEntityMapper<IEntity>
{
    void MapToExistingEntity(IEntity existingEntity, IEntity newEntity);
}