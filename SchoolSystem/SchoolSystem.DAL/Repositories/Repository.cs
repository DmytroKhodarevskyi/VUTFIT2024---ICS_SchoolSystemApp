// namespace SchoolSystem.DAL.Repositories;
namespace DAL.Repositories;

using AutoMapper;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly IMapper _mapper;

    public Repository(DbContext dbContext, IMapper mapper)
    {
        _dbSet = dbContext.Set<TEntity>();
        _mapper = mapper;
    }

    public IQueryable<TEntity> Get() => _dbSet;

    public async ValueTask<bool> ExistsAsync(TEntity entity)
        => entity.Id != Guid.Empty && await _dbSet.AnyAsync(e => e.Id == entity.Id);

    public async Task<TEntity> InsertAsync(TEntity entity)
        => (await _dbSet.AddAsync(entity)).Entity;

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        TEntity existingEntity = await _dbSet.SingleAsync(e => e.Id == entity.Id);
        // Using AutoMapper to map changes from the provided entity to the existing entity.
        _mapper.Map(entity, existingEntity);
        return existingEntity;
    }

    public void Delete(Guid entityId)
        => _dbSet.Remove(_dbSet.Single(i => i.Id == entityId));
}