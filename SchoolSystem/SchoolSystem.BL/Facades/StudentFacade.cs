using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class StudentFacade : CRUDFacade<StudentEntity, studentListModel, studentDetailedModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public StudentFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
    }
    public async Task<studentDetailedModel> GetStudentByNameSurname(string Name, string Surname)
    {
        await using var uow = _unitOfWorkFactory.Create();
        // await using var uow = UnitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<StudentEntity>().Get()
            .Where(x => x.Name == Name && x.Surname == Surname);

        // return await Mapper.ProjectTo<studentDetailedModel>(dbSet).ToListAsync().ConfigureAwait(false);
        return _mapper.Map<studentDetailedModel>(dbSet);
    }
    
    public async Task<IEnumerable<studentListModel>> GetStudentsByNameAsync(string name)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var query = uow.GetRepository<StudentEntity>()
            .Get()
            .Where(student => student.Name.Contains(name));

        return await _mapper.ProjectTo<studentListModel>(query).ToArrayAsync();
    }
}
