using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Facades.Interfaces;

public interface IStudentFacade : IFacade<StudentEntity, StudentListModel, StudentDetailedModel>
{
    Task<IEnumerable<StudentListModel>> GetStudentsByNameAsync(string name);
    Task<StudentListModel> GetStudentByNameAsync(string name);
    
}