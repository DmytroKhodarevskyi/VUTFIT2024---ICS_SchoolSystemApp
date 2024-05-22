using System;
using System.Collections.Generic;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Seeds;
using DAL.Entities;
using DAL.Enums;
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
            Description = "Test",
            StudentId = EvaluationSeeds.Evaluation1.StudentId,
            ActivityId = EvaluationSeeds.Evaluation1.ActivityId
        };
        
        // Act
        SchoolSystemDbContextSUT.Evaluations.Add(entity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();
        
        // Assert
        await using var dbContext = await DbContextFactory.CreateDbContextAsync();
        var actualEvaluationEntity = await dbContext.Evaluations.SingleAsync(e => e.Id == entity.Id);
        DeepAssert.Equal(entity, actualEvaluationEntity);
    }
    
    [Fact]
    
    public async Task AddEvaluationWithStudentAndActivity()
    {
        var evaluationEntity = EvaluationSeeds.EmptyEvaluationEntity with
        {
            Id = Guid.NewGuid(),
            Score = 2,
            Description = "Test",
            StudentId = StudentSeeds.Student1.Id,
            ActivityId = ActivitySeeds.Activity1.Id
        };
        
        // Act
        SchoolSystemDbContextSUT.Evaluations.Add(evaluationEntity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();
        
        // Assert
        await using var dbContext = await DbContextFactory.CreateDbContextAsync();
        var actualEvaluationEntity = await dbContext.Evaluations
            .Include(e => e.Student)
            .Include(e => e.Activity)
            .SingleAsync(e => e.Id == evaluationEntity.Id);

        Assert.Equal(evaluationEntity.StudentId, actualEvaluationEntity.StudentId);

    }
    
    [Fact]
    public async Task FindEvaluationById()
    {
        // Arrange: Using a predefined seed
        var entity = EvaluationSeeds.Evaluation1;
        
        // Act
        var actualEvaluationEntity = await SchoolSystemDbContextSUT.Evaluations.FindAsync(entity.Id);
        
        // Assert
        Assert.Equal(entity.Id, actualEvaluationEntity.Id);
    }
    
    [Fact]
    public async Task UpdateEvaluation()
    {
        // Arrange: Using a predefined seed intended for update
        var evaluationToUpdate = await SchoolSystemDbContextSUT.Evaluations.FindAsync(EvaluationSeeds.Evaluation1.Id);

        string newDescription = "Updated";
        evaluationToUpdate.Description = newDescription;
        
        // Act
        SchoolSystemDbContextSUT.Evaluations.Update(evaluationToUpdate);
        await SchoolSystemDbContextSUT.SaveChangesAsync();
        
        // Assert
        await using var dbContext = await DbContextFactory.CreateDbContextAsync();
        var actualEvaluationEntity = await dbContext.Evaluations.SingleAsync(e => e.Id == evaluationToUpdate.Id);
        Assert.Equal(evaluationToUpdate.Description, newDescription);
    }
    
    [Fact]
    public async Task DeleteEvaluation()
    {
        // Arrange: Using a predefined seed intended for deletion
        var evaluationToDelete = EvaluationSeeds.EvaluationEntityDelete2;

        // Act
        SchoolSystemDbContextSUT.Evaluations.Remove(evaluationToDelete);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbContext = await DbContextFactory.CreateDbContextAsync();
        var deletedEvaluation = await dbContext.Evaluations.FindAsync(evaluationToDelete.Id);
        Assert.Null(deletedEvaluation);
    }
    
   

}