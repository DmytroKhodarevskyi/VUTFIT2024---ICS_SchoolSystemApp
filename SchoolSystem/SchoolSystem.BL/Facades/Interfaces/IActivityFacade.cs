using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Facades.Interfaces;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    
    Task<IEnumerable<ActivityListModel>> GetAsyncFilter(Guid studentId, DateTime? start, DateTime? end, int tag, Guid? subjectId);
    
    Task<IEnumerable<ActivityListModel>> GetAsyncListBySubject(Guid subjectId);
    
    
}


