using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public sealed class EvaluationFacadeTests : CRUDFacadeTestsBase
{
    private readonly EvaluationFacade _evaluationFacadeSUT;

    public EvaluationFacadeTests(ITestOutputHelper output) : base(output)
    {
        _evaluationFacadeSUT = new EvaluationFacade(UnitOfWorkFactory, EvaluationMapper);
    }
    
    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new EvaluationDetailModel()
        {
            Id = Guid.Empty,
            StudentId = EvaluationSeeds.Evaluation1.StudentId,
            ActivityId = EvaluationSeeds.Evaluation1.ActivityId,
            Score = 5,
            Description = "Good job",
        };

        var _ = await _evaluationFacadeSUT.SaveAsync(model);
    }
    
    [Fact]
    
    public async Task GetAll_Single_SeededEvaluation1()
    {
        var evaluations = await _evaluationFacadeSUT.GetAsync();
       

        DeepAssert.Contains(EvaluationMapper.MapToListModel(EvaluationSeeds.Evaluation1), evaluations);
    }
    
    [Fact]
    public async Task GetById_SeededEvaluation1()
    {
        var evaluation = await _evaluationFacadeSUT.GetAsync(EvaluationSeeds.Evaluation1.Id);

        DeepAssert.Equal(EvaluationMapper.MapToDetailModel(EvaluationSeeds.Evaluation1), evaluation);
    }


    [Fact]
    public async Task GetById_NonExistent()
    {
        var evaluation = await _evaluationFacadeSUT.GetAsync(EvaluationSeeds.EmptyEvaluationEntity.Id);

        Assert.Null(evaluation);
    }
    
    [Fact]
    public async Task Update_ExistingItem()
    {
        var model = new EvaluationDetailModel()
        {
            Id = EvaluationSeeds.Evaluation1.Id,
            StudentId = EvaluationSeeds.Evaluation1.StudentId,
            ActivityId = EvaluationSeeds.Evaluation1.ActivityId,
            Score = 5,
            Description = "Good job",
        };

        var _ = await _evaluationFacadeSUT.SaveAsync(model);
        
        var evaluation = await _evaluationFacadeSUT.GetAsync(EvaluationSeeds.Evaluation1.Id);
        
        DeepAssert.Equal(model, evaluation);
    }
    
    [Fact]
    public async Task GetAsyncListByStudent()
    {
        var evaluations = await _evaluationFacadeSUT.GetAsyncListByStudent(EvaluationSeeds.Evaluation1.StudentId);
        var evaluation = evaluations.Single(i => i.Id == EvaluationSeeds.Evaluation1.Id);

        DeepAssert.Equal( EvaluationMapper.MapToListModel(EvaluationSeeds.Evaluation1), evaluation);
        
    }
    
    [Fact]
    
    public async Task GetAsyncListByActivity()
    {
        var evaluations = await _evaluationFacadeSUT.GetAsyncListByActivity(EvaluationSeeds.Evaluation1.ActivityId);
        var evaluation = evaluations.Single(i => i.Id == EvaluationSeeds.Evaluation1.Id);

        DeepAssert.Equal(EvaluationMapper.MapToListModel(EvaluationSeeds.Evaluation1), evaluation);
    }

    [Fact]
    public async Task GetAsyncListByActivityAndStudent()
    {
        var evaluations =
            await _evaluationFacadeSUT.GetAsyncListByActivityAndStudent(EvaluationSeeds.Evaluation1.ActivityId,
                EvaluationSeeds.Evaluation1.StudentId);
        var evaluation = evaluations.Single(i => i.Id == EvaluationSeeds.Evaluation1.Id);
        
        DeepAssert.Equal(EvaluationMapper.MapToListModel(EvaluationSeeds.Evaluation1), evaluation);
    }

    [Fact]
    public async Task Delete_ExistingItem()
    {
        await _evaluationFacadeSUT.DeleteAsync(EvaluationSeeds.Evaluation1.Id);
        
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Evaluations.AnyAsync(i => i.Id == EvaluationSeeds.Evaluation1.Id));
    }
    
}