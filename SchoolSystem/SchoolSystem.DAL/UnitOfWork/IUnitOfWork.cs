using DAL.Entities;
using DAL.Mappers;
using DAL.Repositories;

namespace DAL.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
    Task CommitAsync();
}