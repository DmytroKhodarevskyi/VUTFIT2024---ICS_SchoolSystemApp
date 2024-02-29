using System;
using System.Collections.Generic;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Seeds;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.DAL.Tests;

public class DbContextEvaluationTest : DbContextTestsBase
{
    public DbContextEvaluationTest(ITestOutputHelper output) : base(output)
    {
    }
    [Fact]
    public async Task AddNewEvaluation()
    {
        // Arrange
        var entity = EvaluationSeeds.EmptyEvaluationEntity with
        {
            Score = 2,
            Description = "Test"
        };
        
        // Act
        SchoolSystemDbContextSUT.Evaluations.Add(entity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();
        
        // Assert
        await using var dbContext = await DbContextFactory.CreateDbContextAsync();
        var actualEvaluationEntity = await dbContext.Evaluations.SingleAsync(e => e.Id == entity.Id);
        DeepAssert.Equal(entity, actualEvaluationEntity);
    }
    
    
}

