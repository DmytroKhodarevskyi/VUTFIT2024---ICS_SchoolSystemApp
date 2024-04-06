using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Entities;
using DAL.Repositories;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;

namespace SchoolSystem.BL.Facades;

public class CRUDFacade<TEntity, TListModel, TDetailModel>(
    IMapper mapper,
    IUnitOfWorkFactory unitOfWorkFactory
)
    where TEntity : class, IEntity
    where TListModel : class, IModel
    where TDetailModel : class, IModel
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory = unitOfWorkFactory;
    
    public async Task DeleteAsync(TDetailModel model) => await this.DeleteAsync(model.Id);

    public async Task DeleteAsync(Guid id)
    {
        await using var uow = _unitOfWorkFactory.Create();
        uow.GetRepository<TEntity>().Delete(id);
        await uow.CommitAsync().ConfigureAwait(false);
    }

    public async Task<TDetailModel?> GetAsync(Guid id)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TEntity>()
            .Get()
            .Where(e => e.Id == id);
        return await _mapper.ProjectTo<TDetailModel>(query).SingleOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<TListModel>> GetAsync()
    {
        await using var uow = _unitOfWorkFactory.Create();
        var query = uow
            .GetRepository<TEntity>()
            .Get();
        return await _mapper.ProjectTo<TListModel>(query).ToArrayAsync().ConfigureAwait(false);
    }

    public async Task<TDetailModel> SaveAsync(TDetailModel model)
    {
        TDetailModel result;
        
    
        IUnitOfWork uow = _unitOfWorkFactory.Create();
        IRepository<TEntity> repository = uow.GetRepository<TEntity>();
        var entity = _mapper.Map<TEntity>(model);

        if (await repository.ExistsAsync(entity).ConfigureAwait(false))
        {
            TEntity updatedEntity = await repository.UpdateAsync(entity).ConfigureAwait(false);
            result = _mapper.Map<TDetailModel>(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            TEntity insertedEntity = await repository.InsertAsync(entity);
            result = _mapper.Map<TDetailModel>(insertedEntity);
        }

        await uow.CommitAsync().ConfigureAwait(false);

        return result;
    }
}

    
