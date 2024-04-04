using Microsoft.EntityFrameworkCore;
using SchoolSystem.DAL;
namespace DAL.UnitOfWork;

public class UnitOfWorkFactory(IDbContextFactory<SchoolSystemDbContext> dbContextFactory) : IUnitOfWorkFactory
{
    public IUnitOfWork Create() => new UnitOfWork(dbContextFactory.CreateDbContext());
}