namespace SchoolSystem.BL.Mappers;

// Based on ICS Cookbook ( GPL-3.0 license ): 
// https://github.com/nesfit/ICS/tree/master?tab=GPL-3.0-1-ov-file#readme

public abstract class
    ModelMapperBase<TEntity, TListModel, TDetailModel> : IModelMapper<TEntity, TListModel, TDetailModel>
{
    public abstract TListModel MapToListModel(TEntity? entity);

    public IEnumerable<TListModel> MapToListModel(IEnumerable<TEntity> entities)
        => entities.Select(MapToListModel);

    public abstract TDetailModel MapToDetailModel(TEntity entity);
    public abstract TEntity MapToEntity(TDetailModel model);
}
