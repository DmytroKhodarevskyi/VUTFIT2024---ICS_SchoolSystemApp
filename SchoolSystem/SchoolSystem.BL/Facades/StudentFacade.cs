using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class StudentFacade : CrudFacade<,,,>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    public StudentFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
    }
    public async Task<IEnumerable<StudentDetailedModel>> GetStudentByNameSurname(string Name, string Surname)
    {
        await using var uow = _unitOfWorkFactory.Create();
        // await using var uow = UnitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<StudentEntity>().Get()
            .Where(x => x.Name == Name && x.Surname == Surname);

         return await _mapper.ProjectTo<StudentDetailedModel>(dbSet).ToArrayAsync().ConfigureAwait(false);
        //return _mapper.Map<StudentDetailedModel>(dbSet);
    }
    
    public async Task<IEnumerable<StudentListModel>> GetStudentsByNameAsync(string name)
    {
        await using var uow = _unitOfWorkFactory.Create();

        var query = uow.GetRepository<StudentEntity>()
            .Get()
            .Where(student => student.Name.Contains(name));

        return await _mapper.ProjectTo<StudentListModel>(query).ToArrayAsync();
    }
}
