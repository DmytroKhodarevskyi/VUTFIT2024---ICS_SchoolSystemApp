using DAL.Entities;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Facades.Interfaces;

public interface IEvaluationFacade : IFacade<EvaluationEntity, EvaluationListModel, EvaluationDetailModel>
{
    
    Task<IEnumerable<EvaluationListModel>> GetAsyncListByStudent(Guid? studentId);
    Task<IEnumerable<EvaluationListModel>> GetAsyncListByActivity(Guid? activityId);
    Task<IEnumerable<EvaluationListModel>> GetAsyncListByActivityAndStudent(Guid? activityId, Guid? studentId);
}