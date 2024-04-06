using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.UnitOfWork;

namespace SchoolSystem.BL.Facades;

public class EvaluationFacade : CRUDFacade<EvaluationEntity, EvaluationListModel, EvaluationDetailModel>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    
    public EvaluationFacade(IMapper mapper, IUnitOfWorkFactory unitOfWorkFactory) : base(mapper, unitOfWorkFactory)
    {
        _mapper = mapper;
        _unitOfWorkFactory = unitOfWorkFactory;
    }
    
    public async Task<List<EvaluationListModel>> GetPassedScores(int minimum)
    {
        await using var uow = _unitOfWorkFactory.Create();
        // await using var uow = UnitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<EvaluationEntity>().Get()
            .Where(x => x.Score >= minimum);

        return await _mapper.ProjectTo<EvaluationListModel>(dbSet).ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<EvaluationListModel>> GetStudentScore(string Name, string Surname)
    {
        await using var uow = _unitOfWorkFactory.Create();
        // await using var uow = UnitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<EvaluationEntity>().Get()
            .Include(x => x.Student)
            .Where(x => x.Student != null && x.Student.Name == Name && x.Student.Surname == Surname);

        // var model = _mapper.ProjectTo<EvaluationListModel>(dbSet).ToListAsync().ConfigureAwait(false);
        return await _mapper.ProjectTo<EvaluationListModel>(dbSet).ToListAsync().ConfigureAwait(false);
    }
    
}