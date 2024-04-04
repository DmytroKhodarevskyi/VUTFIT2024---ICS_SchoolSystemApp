using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.DAL.UnitOfWork;

public class UnitOfWorkFactory(IDbContextFactory<SchoolSystemDbContext> dbContextFactory) : IUnitOfWorkFactory
{
    public IUnitOfWork Create() => new UnitOfWork(dbContextFactory.CreateDbContext());
}