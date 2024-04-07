// using System;
// using System.Collections.Generic;
// using SchoolSystem.Common.Tests;
// using SchoolSystem.Common.Tests.Seeds;
// using DAL.Entities;
// using DAL.Enums;
// using Microsoft.EntityFrameworkCore;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace SchoolSystem.DAL.Tests;
//
// public class DbContextEvaluationTest : DbContextTestsBase
// {
//     public DbContextEvaluationTest(ITestOutputHelper output) : base(output)
//     {
//     }
//     
//     [Fact]
//     public async Task AddNewEvaluation()
//     {
//         // Arrange
//         var entity = EvaluationSeeds.EmptyEvaluationEntity with
//         {
//             Score = 2,
//             Description = "Test"
//         };
//         
//         // Act
//         SchoolSystemDbContextSUT.Evaluations.Add(entity);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();
//         
//         // Assert
//         await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//         var actualEvaluationEntity = await dbContext.Evaluations.SingleAsync(e => e.Id == entity.Id);
//         DeepAssert.Equal(entity, actualEvaluationEntity);
//     }
//     
//     [Fact]
//     
//     public async Task AddEvaluationWithStudentAndActivity()
//     {
//         var evaluationEntity = EvaluationSeeds.EmptyEvaluationEntity with
//         {
//             Score = 2,
//             Description = "Test",
//             Student = StudentSeeds.EmptyStudentEntity with
//             {
//                 Name = "Denys",
//                 Surname = "Chernenko",
//                 
//             },
//             Activity = ActivitySeeds.EmptyActivity with
//             {
//                 Start = new DateTime(2020, 03, 24, 03, 34, 50),
//                 End = new DateTime(2020, 03, 24, 08, 30, 50),
//                 Room = Room.D106 ,
//                 Tag = 1,
//                 Description = "Test",
//             }
//         };
//         
//         // Act
//         SchoolSystemDbContextSUT.Evaluations.Add(evaluationEntity);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();
//         
//         // Assert
//         await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//         var actualEvaluationEntity = await dbContext.Evaluations
//             .Include(e => e.Student)
//             .Include(e => e.Activity)
//             .SingleAsync(e => e.Id == evaluationEntity.Id);
//
//         DeepAssert.Equal(evaluationEntity, actualEvaluationEntity);
//
//     }
//
//     [Fact]
//
//     public async Task Update_Evaluation_Persisted()
//     {
//         var baseEvaluation = EvaluationSeeds.EmptyEvaluationEntity;
//         var evaluation = baseEvaluation with
//         {
//             Description = baseEvaluation.Description + "Updated",
//         };
//         
//         
//         SchoolSystemDbContextSUT.Evaluations.Add(evaluation);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();
//         
//         await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//         var actualEvaluationEntity = await dbContext.Evaluations.SingleAsync(e => e.Id == evaluation.Id);
//         DeepAssert.Equal(evaluation, actualEvaluationEntity);
//     }
//     
//     [Fact]
//     public async Task FindEvaluationById()
//     {
//         // Arrange: Using a predefined seed
//         var entity = EvaluationSeeds.Evaluation1;
//         
//         // Act
//         var actualEvaluationEntity = await SchoolSystemDbContextSUT.Evaluations.FindAsync(entity.Id);
//         
//         // Assert
//         DeepAssert.Equal(entity, actualEvaluationEntity);
//     }
//     
//     [Fact]
//     public async Task UpdateEvaluation()
//     {
//         // Arrange: Using a predefined seed intended for update
//         var entity = EvaluationSeeds.EvaluationEntityUpdated1;
//         var updatedEntity = entity with
//         {
//             Score = 5,
//             Description = "Updated"
//         };
//         
//         // Act
//         SchoolSystemDbContextSUT.Evaluations.Update(updatedEntity);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();
//         
//         // Assert
//         await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//         var actualEvaluationEntity = await dbContext.Evaluations.SingleAsync(e => e.Id == entity.Id);
//         Assert.Equal(updatedEntity, actualEvaluationEntity);
//     }
//     
//     [Fact]
//     public async Task DeleteEvaluation()
//     {
//         // Arrange: Using a predefined seed intended for deletion
//         var evaluationToDelete = EvaluationSeeds.EvaluationEntityDelete2;
//
//         // Act
//         SchoolSystemDbContextSUT.Evaluations.Remove(evaluationToDelete);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();
//
//         // Assert
//         await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//         var deletedEvaluation = await dbContext.Evaluations.FindAsync(evaluationToDelete.Id);
//         Assert.Null(deletedEvaluation);
//     }
//     
//     [Fact]
//     public async Task DeleteEvaluationById()
//     {
//         // Arrange: Using a predefined seed intended for deletion
//         var evaluationToDelete = EvaluationSeeds.EvaluationEntityDelete2;
//
//         // Act
//         SchoolSystemDbContextSUT.Evaluations.Remove(evaluationToDelete);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();
//
//         // Assert
//         await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//         var deletedEvaluation = await dbContext.Evaluations.FindAsync(evaluationToDelete.Id);
//         Assert.Null(deletedEvaluation);
//     }
//
// }