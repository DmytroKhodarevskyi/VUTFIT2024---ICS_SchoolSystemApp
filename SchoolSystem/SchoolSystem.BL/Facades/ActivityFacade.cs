using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.Enums;
using DAL.Mappers;
using DAL.UnitOfWork;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Mappers;

namespace SchoolSystem.BL.Facades;

public class ActivityFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    ActivityModelMapper mapper)
    :
        CrudFacade<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>(
            unitOfWorkFactory, mapper), IActivityFacade;
    
    public async Task<List<ActivityListModel>> GetRoomActivities(Room room)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<ActivityEntity>().Get()
            .Where(x => x.Room == room);

        return await _mapper.ProjectTo<ActivityListModel>(dbSet).ToListAsync().ConfigureAwait(false);
    }
    
    public async Task<List<ActivityListModel>> GetActivitiesAfter(DateTime after)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<ActivityEntity>().Get()
            .Where(x => x.Start > after);

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