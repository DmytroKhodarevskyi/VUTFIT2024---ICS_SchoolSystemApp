using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.Mappers;
using DAL.UnitOfWork;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Mappers;

namespace SchoolSystem.BL.Facades;

public class EvaluationFacade :
    CrudFacade<EvaluationEntity, EvaluationListModel, EvaluationDetailModel, EvaluationEntityMapper>, IEvaluationFacade
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly EvaluationModelMapper _mapper;

    protected override ICollection<string> IncludesNavigationPathDetail =>
        new[]
        {
            $"{nameof(EvaluationEntity.Activity)}",
            $"{nameof(EvaluationEntity.Student)}"
        };

    public EvaluationFacade(IUnitOfWorkFactory unitOfWorkFactory, EvaluationModelMapper mapper)
        : base(unitOfWorkFactory, mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EvaluationListModel>> GetAsyncListByStudent(Guid? studentId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        if (studentId is null)
        {
            return new List<EvaluationListModel>();
        }

        List<EvaluationEntity> query = uow.GetRepository<EvaluationEntity, EvaluationEntityMapper>().Get()
            .Where(e => e.StudentId == studentId)
            .ToList();

        return ModelMapper.MapToListModel(query);
    }

    public async Task<IEnumerable<EvaluationListModel>> GetAsyncListByActivity(Guid activityId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        List<EvaluationEntity> query = uow.GetRepository<EvaluationEntity, EvaluationEntityMapper>().Get()
            .Where(e => e.ActivityId == activityId)
            .ToList();

        return ModelMapper.MapToListModel(query);
    }

    public async Task<IEnumerable<EvaluationListModel>> GetAsyncListByActivityAndStudent(Guid? activityId,
        Guid? studentId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        if (activityId is null || studentId is null)
        {
            return new List<EvaluationListModel>();
        }

        List<EvaluationEntity> query = uow.GetRepository<EvaluationEntity, EvaluationEntityMapper>().Get()
            .Where(e => e.ActivityId == activityId && e.StudentId == studentId)
            .ToList();

        return ModelMapper.MapToListModel(query);
    }

    public async Task<IEnumerable<EvaluationListModel>> GetAsyncListBySubjectAndStudent(Guid? subjectId,
        Guid? studentId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        if (subjectId is null || studentId is null)
        {
            return new List<EvaluationListModel>();
        }

        List<EvaluationEntity> query = uow.GetRepository<EvaluationEntity, EvaluationEntityMapper>().Get()
            .Where(e => e.Activity!.SubjectId == subjectId && e.StudentId == studentId)
            .ToList();

        return ModelMapper.MapToListModel(query);
    }
    
    public async Task<EvaluationDetailModel> GetEvaluationByStudentAndActivity(Guid studentId, Guid activityId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        EvaluationEntity? query = uow.GetRepository<EvaluationEntity, EvaluationEntityMapper>().Get()
            .FirstOrDefault(e => e.StudentId == studentId && e.ActivityId == activityId);

        return (query is null
            ? null
            : ModelMapper.MapToDetailModel(query))!;
    }
}