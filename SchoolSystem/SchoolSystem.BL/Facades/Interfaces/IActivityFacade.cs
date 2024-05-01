using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Facades.Interfaces;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    
    Task<IEnumerable<ActivityListModel>> GetAsyncFilter(DateTime? start, DateTime? end);
    
    Task<IEnumerable<ActivityListModel>> GetAsyncListBySubject(Guid subjectId);
    
    enum Interval
    {
        All,
        Daily,
        Weekly,
        This_Month,
        Last_Month,
        Yearly
    }
}


