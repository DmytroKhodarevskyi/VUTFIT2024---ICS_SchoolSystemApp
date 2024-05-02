using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.Mappers;
using DAL.UnitOfWork;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Mappers;


namespace SchoolSystem.BL.Facades;


public class SubjectFacade(IUnitOfWorkFactory unitOfWorkFactory, SubjectModelMapper mapper)
    : CrudFacade<SubjectEntity, SubjectListModel, SubjectDetailedModel, SubjectEntityMapper>(unitOfWorkFactory,
        mapper), ISubjectFacade
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory = unitOfWorkFactory;
    public async Task<IEnumerable<SubjectListModel>> GetSubjectsByName(string name)
    {
        await using var uow = unitOfWorkFactory.Create();

        var dbSet = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get()
            .Where(x => x.Name == name).ToList();
        
        return mapper.MapToListModel(dbSet);
    }
    
    public async Task<IEnumerable<SubjectListModel>> GetSubjectsByAbrAsync(string name)
    {
        await using var uow = unitOfWorkFactory.Create();

        var dbSet = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get()
            .Where(x => x.Abbreviation == name).ToList();
        
        return mapper.MapToListModel(dbSet);
    }
    
    
    public async Task<IEnumerable<SubjectListModel>> GetSubjectsByNameAsync(string name)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var dbSet = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get()
            .Where(subject => subject.Name.Contains(name)).ToList();

        return mapper.MapToListModel(dbSet);
    }
    
    public async Task<IEnumerable<SubjectListModel>> GetSubjectsByAbbrAsync(string abbreviation)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var dbSet = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get()
            .Where(subject => subject.Abbreviation.Contains(abbreviation)).ToList();

        return mapper.MapToListModel(dbSet);
    }
    
    public async Task AddActivityToSubject(Guid subjectId, Guid activityId)
    {
        await using var uow = _unitOfWorkFactory.Create();
        var subject = uow.GetRepository<SubjectEntity, SubjectEntityMapper>().Get().FirstOrDefault(subject => subject.Id == subjectId);
        var activity = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get().FirstOrDefault(activity => activity.Id == activityId);
        subject!.Activities.Add(activity!);
        await uow.CommitAsync();
    }
    
    public async Task<SubjectListModel> GetSubjectByAbbrAsync(string abbreviation)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var dbSet = uow.GetRepository<SubjectEntity, SubjectEntityMapper>()
            .Get().FirstOrDefault(subject => subject.Abbreviation.Contains(abbreviation));

        return mapper.MapToListModel(dbSet);
    }
}