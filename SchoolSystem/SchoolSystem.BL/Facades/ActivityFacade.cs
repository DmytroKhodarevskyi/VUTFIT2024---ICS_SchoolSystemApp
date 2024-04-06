using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.Enums;
using DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class ActivityFacade : CRUDFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    
    public ActivityFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
    }
    
    public async Task<IEnumerable<ActivityListModel>> GetRoomActivities(Room room)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<ActivityEntity>().Get()
            .Where(x => x.Room == room);

        // return await _mapper.ProjectTo<ActivityListModel>(dbSet).ToListAsync().ConfigureAwait(false);
        return await _mapper.ProjectTo<ActivityListModel>(dbSet).ToArrayAsync();
    }
    
    public async Task<List<ActivityListModel>> GetActivitiesAfter(DateTime after)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<ActivityEntity>().Get()
            .Where(x => x.Start > after);

        return await _mapper.ProjectTo<ActivityListModel>(dbSet).ToListAsync().ConfigureAwait(false);
    }
    
    public async Task<List<ActivityListModel>> GetActivitiesBefore(DateTime before)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<ActivityEntity>().Get()
            .Where(x => x.Start < before);

        return await _mapper.ProjectTo<ActivityListModel>(dbSet).ToListAsync().ConfigureAwait(false);
    }
    
    public async Task<List<ActivityListModel>> GetActivitiesByTag(int Tag)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<ActivityEntity>().Get()
            .Where(x => x.Tag == Tag);

        return await _mapper.ProjectTo<ActivityListModel>(dbSet).ToListAsync().ConfigureAwait(false);
    }

    
}