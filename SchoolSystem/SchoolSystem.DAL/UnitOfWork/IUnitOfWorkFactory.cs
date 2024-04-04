namespace SchoolSystem.DAL.UnitOfWork;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}