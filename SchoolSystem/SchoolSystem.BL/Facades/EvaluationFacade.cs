using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Models;
using DAL.Entities;
using DAL.Mappers;
using DAL.UnitOfWork;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Mappers;

namespace SchoolSystem.BL.Facades;

public class EvaluationFacade(
    IUnitOfWorkFactory unitOfWorkFactory,
    EvaluationModelMapper mapper)
    : 
        CrudFacade<EvaluationEntity, EvaluationListModel, EvaluationDetailModel, EvaluationEntityMapper>(
            unitOfWorkFactory, mapper), IEvaluationFacade;
    
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
    
    public async Task<List<EvaluationListModel>> GetStudentScoreForSubject(string Name, string Surname, string Subject)
    {
        await using var uow = _unitOfWorkFactory.Create();
        // await using var uow = UnitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<EvaluationEntity>().Get()
            .Include(x => x.Student)
            .Include(x => x.Activity)
            .Where(x => x.Student != null && x.Student.Name == Name && x.Student.Surname == Surname && x.Activity != null && x.Activity!.Subject!.Name == Subject);

        return await _mapper.ProjectTo<EvaluationListModel>(dbSet).ToListAsync().ConfigureAwait(false);
    }
    
    public async Task<List<EvaluationListModel>> GetTopScores(int n)
    {
        await using var uow = _unitOfWorkFactory.Create();
        // await using var uow = UnitOfWorkFactory.Create();
        var dbSet = uow.GetRepository<EvaluationEntity>().Get()
            .OrderByDescending(x => x.Score)
            .Take(n);

        return await _mapper.ProjectTo<EvaluationListModel>(dbSet).ToListAsync().ConfigureAwait(false);
    }
    
    public async Task<EvaluationDetailModel> GetFinalEvaluationOfStudentForSubject(string Name, string Surname, string Subject)
    {
        await using var uow = _unitOfWorkFactory.Create();
        // Retrieve evaluations for the student and subject
        var evaluations = uow.GetRepository<EvaluationEntity>().Get().ToList();
       
        // Calculate the final evaluation, e.g., as an average of the scores
        int finalEvaluationScore = evaluations.Sum(x => x.Score); // This assumes 'Score' is a property of EvaluationEntity. Adjust accordingly.
        // Assuming EvaluationDetailModel has a constructor or properties that can be set directly. Adjust according to your actual model structure.
        var finalEvaluation = new EvaluationDetailModel(finalEvaluationScore,"Final Evaluation");
    
        return finalEvaluation;
    }

   
    
}