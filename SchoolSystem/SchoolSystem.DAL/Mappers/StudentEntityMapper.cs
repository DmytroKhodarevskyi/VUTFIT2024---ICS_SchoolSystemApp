
using DAL.Entities;

namespace DAL.Mappers;

public class StudentEntityMapper : IEntityMapper<StudentEntity>
{
    public void MapToExistingEntity(StudentEntity existingEntity, StudentEntity newEntity)
    {
        existingEntity.Name = newEntity.Name;
        existingEntity.Surname = newEntity.Surname;
        existingEntity.Photo = newEntity.Photo;
    }
}
