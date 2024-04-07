
namespace SchoolSystem.BL.Facades;

// Based on ICS Cookbook ( GPL-3.0 license ): 
// https://github.com/nesfit/ICS/tree/master?tab=GPL-3.0-1-ov-file#readme

public interface IFacade<TEntity, TListModel, TDetailModel>
    where TEntity : class, IEntity
    where TListModel : IModel
    where TDetailModel : class, IModel
{
    Task DeleteAsync(Guid id);

    Task<TDetailModel?> GetAsync(Guid id);

    Task<IEnumerable<TListModel>> GetAsync();

    Task<TDetailModel> SaveAsync(TDetailModel model);
}
