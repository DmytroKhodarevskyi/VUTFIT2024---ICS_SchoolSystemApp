using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.Enums;
using DAL.Mappers;
using DAL.UnitOfWork;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Mappers;

namespace SchoolSystem.BL.Facades;

public class ActivityFacade : CrudFacade<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>, IActivityFacade
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ActivityModelMapper _mapper;

    public ActivityFacade(IUnitOfWorkFactory unitOfWorkFactory, ActivityModelMapper mapper)
        : base(unitOfWorkFactory, mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }
  
    public async Task<IEnumerable<ActivityListModel>> GetAsyncFilter(Guid studentId, DateTime? start, DateTime? end, int tag, Guid? subjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        query = query.Where(e => e.SubjectId == subjectId);
        if (start != null)
        {
            query = query.Where(e => e.Start >= start);
        }
        if (end != null)
        {
            query = query.Where(e => e.End <= end);
        }
        if(tag != 0)
        {
            query = query.Where(e => e.Tag == tag);
        }
        if (studentId != Guid.Empty )
        {
            query = query.Where(e => e.Evaluations!.Any(e => e.StudentId == studentId));
        }
        query = query.OrderBy(e => e.Start);
    
        var entities = await query.ToListAsync();
       return ModelMapper.MapToListModel(entities);
    }
    
    public async Task<IEnumerable<ActivityListModel>> GetAsyncListBySubject(Guid subjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        List<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get()
            .Where(e => e.SubjectId == subjectId)
            .ToList();
        
        return ModelMapper.MapToListModel(query);
    }
    
    public async Task<ActivityListModel> GetActivityByName(string name)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        ActivityEntity? query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get()
            .FirstOrDefault(e => e.Name == name);
        
        return ModelMapper.MapToListModel(query);
    }

}