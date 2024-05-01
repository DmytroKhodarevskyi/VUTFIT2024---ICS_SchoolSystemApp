using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Facades.Interfaces;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    
    Task<IEnumerable<ActivityListModel>> GetAsyncFilter(DateTime? start, DateTime? end, int Tag);
    Task<IEnumerable<ActivityListModel>> GetAsyncFilterTag(int Tag);


    Task<IEnumerable<ActivityListModel>> GetAsyncListBySubject(Guid subjectId);

    Task<ActivityListModel> GetActivityByName(string name);


    public enum Interval
    {
        NoFilter,
        Last24Hours,
        Last7Days,
        CurrentMonth,
        PreviousMonth,
        LastYear,
    }


    public enum Room
    {
        D104,
        D105,
        D106,
    }
}


