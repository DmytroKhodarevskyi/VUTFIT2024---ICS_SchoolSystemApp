using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public sealed class EvaluationFacadeTests : CRUDFacadeTestsBase
{
    private readonly IEvaluationFacade _evaluationFacadeSUT;

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
            StudentId = StudentSeeds.Student1.Id,
            ActivityId = ActivitySeeds.Activity1.Id,
            Score = 5,
            Description = "Good job",
        };

        var _ = await _evaluationFacadeSUT.SaveAsync(model);
    }
    
    

}