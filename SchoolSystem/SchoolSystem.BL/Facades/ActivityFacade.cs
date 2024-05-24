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

    protected override ICollection<string> IncludesNavigationPathDetail =>
        new[]
        {
            $"{nameof(ActivityEntity.Evaluations)}",
            $"{nameof(ActivityEntity.Subject)}"
        };
    
    public ActivityFacade(IUnitOfWorkFactory unitOfWorkFactory, ActivityModelMapper mapper)
        : base(unitOfWorkFactory, mapper)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _mapper = mapper;
    }
  
    public async Task<IEnumerable<ActivityListModel>> GetAsyncFilter(DateTime? start, DateTime? end,
        int Tag, string selectedSort, bool Descending, bool DoFilter, string Subject)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        if (DoFilter)
        {
            if (start != null)
            {
                query = query.Where(e => e.Start >= start);
            }
            if (end != null)
            {
                query = query.Where(e => e.End <= end);
            }
            if (Tag > 0)
            {
                query = query.Where(e => e.Tag == Tag);
            }

            if (!string.IsNullOrEmpty(Subject))
            {
           
                query = query.Where(e => e.Subject != null && e.Subject.Name == Subject);
           
            }
        }

        if (selectedSort == "Name")
        {
            query = Descending ? query.OrderByDescending(e => e.Name) : query.OrderBy(e => e.Name);
        }
        else if (selectedSort == "Start")
        {
            query = Descending ? query.OrderByDescending(e => e.Start) : query.OrderBy(e => e.Start);
        }
        else if (selectedSort == "End")
        {
            query = Descending ? query.OrderByDescending(e => e.End) : query.OrderBy(e => e.End);
        }
        else if (selectedSort == "Tag")
        {
            query = Descending ? query.OrderByDescending(e => e.Tag) : query.OrderBy(e => e.Tag);
        }
        else if (selectedSort == "Room")
        {
            query = Descending ? query.OrderByDescending(e => e.Room) : query.OrderBy(e => e.Room);
        }


        var entities = await query.ToListAsync();
        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>> GetAsyncFilterTag(int Tag)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ActivityEntity> query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get();

        if (Tag > -1)
        {
            query = query.Where(e => e.Tag == Tag);
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