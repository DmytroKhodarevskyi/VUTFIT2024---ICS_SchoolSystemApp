using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.UnitOfWork;



namespace SchoolSystem.BL.Facades;


public class SubjectFacade : CRUDFacade<SubjectEntity, SubjectListModel, SubjectDetailedModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public SubjectFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<SubjectDetailedModel> GetSubjectByName(string name)
    {
        await using var uow = _unitOfWorkFactory.Create();
     
        var dbSet = uow.GetRepository<SubjectEntity>().Get()
            .Where(x => x.Name == name);
        
        return _mapper.Map<SubjectDetailedModel>(dbSet);
    }
    
    public async Task<SubjectDetailedModel> GetSubjectByAbbr(string abbreviation)
    {
        await using var uow = _unitOfWorkFactory.Create();
     
        var dbSet = uow.GetRepository<SubjectEntity>().Get()
            .Where(x => x.Abbreviation == abbreviation);
        
        return _mapper.Map<SubjectDetailedModel>(dbSet);
    }
    
    
    public async Task<IEnumerable<SubjectListModel>> GetSubjectsByNameAsync(string name)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var query = uow.GetRepository<SubjectEntity>()
            .Get()
            .Where(subject => subject.Name.Contains(name));

        return await _mapper.ProjectTo<SubjectListModel>(query).ToArrayAsync();
    }
    
    public async Task<IEnumerable<SubjectListModel>> GetSubjectsByAbbrAsync(string abbreviation)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var query = uow.GetRepository<SubjectEntity>()
            .Get()
            .Where(subject => subject.Abbreviation.Contains(abbreviation));

        return await _mapper.ProjectTo<SubjectListModel>(query).ToArrayAsync();
    }

    public async Task<IEnumerable<StudentListModel>> GetStudentsByNameSubject(string name)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var query = uow.GetRepository<StudentSubjectEntity>()
            .Get()
            .Where(studentSubject => studentSubject.Subject != null && studentSubject.Subject.Name.Contains(name))
            .Select(studentSubject => studentSubject.Student);

        // Project the student entities to studentListModel using AutoMapper
        var students = await _mapper.ProjectTo<StudentListModel>(query).ToListAsync().ConfigureAwait(false);

        return students;
    }
    
    public async Task<IEnumerable<StudentListModel>> GetStudentsByAbbrSubject(string abbreviation)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var query = uow.GetRepository<StudentSubjectEntity>()
            .Get()
            .Where(studentSubject => studentSubject.Subject != null && studentSubject.Subject.Abbreviation.Contains(abbreviation))
            .Select(studentSubject => studentSubject.Student);

        // Project the student entities to studentListModel using AutoMapper
        var students = await _mapper.ProjectTo<StudentListModel>(query).ToListAsync().ConfigureAwait(false);

        return students;
    }
    
}

    



// public async Task<subjectDetailedModel> GetSubjectByName(string name)
// {
//     await using var uow = _unitOfWorkFactory.Create();
//     
//     // Retrieve the subject entity based on its name
//     var subjectEntity = await uow.GetRepository<SubjectEntity>()
//         .Get()
//         .FirstOrDefaultAsync(x => x.Name == name)
//         .ConfigureAwait(false);
//     
//     // Map the retrieved subject entity to subjectDetailedModel
//     return _mapper.Map<subjectDetailedModel>(subjectEntity);
// }
