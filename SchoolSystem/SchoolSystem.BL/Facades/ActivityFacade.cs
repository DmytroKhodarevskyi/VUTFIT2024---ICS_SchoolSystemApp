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

        

        return ModelMapper.MapToListModel(entities);
    }
    
}