using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Facades.Interfaces;

public interface IStudentFacade : IFacade<StudentEntity, StudentListModel, StudentDetailedModel>
{
    Task<IEnumerable<StudentListModel>> GetStudentsByNameAsync(string name);

    Task AddSubjectToStudentAsync(Guid studentId, Guid subjectId);

    Task<IEnumerable<StudentListModel>> GetStudentsBySubjectAsync(string subjectAbbr);
    
    Task RemoveSubjectFromStudentAsync(Guid studentId, string subjectAbbr);
    Task<StudentListModel> GetStudentByNameAsync(string name);
    
}