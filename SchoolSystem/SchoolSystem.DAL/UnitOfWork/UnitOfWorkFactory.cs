using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.DAL;
namespace DAL.UnitOfWork;

public class UnitOfWorkFactory(IDbContextFactory<SchoolSystemDbContext> dbContextFactory, IMapper mapper) : IUnitOfWorkFactory
{
    public IUnitOfWork Create() => new UnitOfWork(dbContextFactory.CreateDbContext(), mapper);
}