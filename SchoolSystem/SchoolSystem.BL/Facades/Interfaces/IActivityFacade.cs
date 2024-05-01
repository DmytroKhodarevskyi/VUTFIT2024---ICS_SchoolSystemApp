using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Facades.Interfaces;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    
    Task<IEnumerable<ActivityListModel>> GetAsyncFilter(DateTime? start, DateTime? end, int Tag);
    Task<IEnumerable<ActivityListModel>> GetAsyncFilterTag(int Tag);


    Task<IEnumerable<ActivityListModel>> GetAsyncListBySubject(Guid subjectId);

    Task<ActivityListModel> GetActivityByName(string name);

    
    enum Interval
    {
        All,
        Daily,
        Weekly,
        This_Month,
        Last_Month,
        Yearly
    }

    public enum Room
    {
        D104,
        D105,
        D106,
    }
}


