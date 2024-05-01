using DAL.Entities;

namespace DAL.Mappers;

public class StudentSubjectsEntityMapper: IEntityMapper<StudentSubjectEntity>
{
    public void MapToExistingEntity(StudentSubjectEntity existingEntity, StudentSubjectEntity newEntity)
    {
        existingEntity.StudentId = newEntity.StudentId;
        existingEntity.SubjectId = newEntity.SubjectId;
    }
}