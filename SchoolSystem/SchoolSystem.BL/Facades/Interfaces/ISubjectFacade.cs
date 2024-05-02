using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Facades.Interfaces;

public interface ISubjectFacade : IFacade<SubjectEntity, SubjectListModel, SubjectDetailedModel>
{
    Task<SubjectListModel> GetSubjectByAbbrAsync(string abbreviation);
    Task AddActivityToSubject(Guid subjectId, Guid activityId);
    
    Task<IEnumerable<SubjectListModel>> GetSubjectsByName(string name);
}