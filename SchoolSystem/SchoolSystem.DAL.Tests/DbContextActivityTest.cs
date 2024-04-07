// using System;
// using System.Security.Cryptography;
// using System.Threading.Tasks;
// using SchoolSystem.Common.Tests;
// using SchoolSystem.Common.Tests.Seeds;
// using DAL.Entities;
// using Microsoft.EntityFrameworkCore;
// using Xunit;
// using Xunit.Abstractions;
// using DAL.Enums;
//
// namespace SchoolSystem.DAL.Tests;
// // public class DbContextActivityTest(ITestOutputHelper output) : DbContextTestsBase(output)
// public class DbContextActivityTest : DbContextTestsBase
// {
//     
//     public DbContextActivityTest(ITestOutputHelper output) : base(output)
//     {
//     }
//
//     
//     [Fact]
//     public async Task AddNewActivity()
//     {
//         // Arrange
//         var entity = ActivitySeeds.EmptyActivity with
//         {
//             Start = new DateTime(2020, 03, 24, 03, 34, 50),
//             End = new DateTime(2020, 03, 24, 08, 30, 50),
//             Room = Room.D105,
//             Tag = 1,
//             Description = "Test of the activity entity"
//         };
//         
//         // Act
//         SchoolSystemDbContextSUT.Activities.Add(entity);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();
//         
//         // Assert
//         await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//         var actualActivityEntity = await dbContext.Activities.SingleAsync(e => e.Id == entity.Id);
//         DeepAssert.Equal(entity, actualActivityEntity);
//     }
//
//     [Fact]
//     public async Task AddNewActivityWithSubject()
//     {
//         
//         // Arrange
//         var entity = ActivitySeeds.EmptyActivity with
//         {
//             Start = new DateTime(2020, 03, 24, 03, 34, 50),
//             End = new DateTime(2020, 03, 24, 08, 30, 50),
//             Room = Room.D105,
//             Tag = 1,
//             Description = "Test",
//             // Subject = SubjectSeeds.IZP
//             Subject = SubjectSeeds.EmptySubject with
//             {
//                 Name = "Math",
//                 Abbreviation = "MTH"
//             }
//         };
//         
//
//         // Act
//         SchoolSystemDbContextSUT.Activities.Add(entity);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();  
//
//             await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//             var actualActivityEntity = await dbContext.Activities
//                 .Include(i => i.Subject)
//                 .SingleAsync(i => i.Id == entity.Id);
//             DeepAssert.Equal(entity, actualActivityEntity);
//         // await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//     }
//     
//     [Fact]
//     public async Task AddNewActivityWithEvaluations()
//     {
//         // Arrange
//         var entity = ActivitySeeds.EmptyActivity with
//         {
//             Name = "Test",
//             Start = new DateTime(2020, 03, 24, 03, 34, 50),
//             End = new DateTime(2020, 03, 24, 08, 30, 50),
//             Room = Room.D105,
//             Tag = 1,
//             Description = "Test of the activity entity",
//             Evaluations = new List<EvaluationEntity>
//             {
//                 EvaluationSeeds.EmptyEvaluationEntity with
//                 {
//                     Score = 2,
//                     Description = "Test"
//                 }
//             }
//         };
//         
//         // Act
//         SchoolSystemDbContextSUT.Activities.Add(entity);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();
//         
//         // Assert
//         await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//         var actualActivityEntity = await dbContext.Activities
//             .Include(i => i.Evaluations)
//             .SingleAsync(i => i.Id == entity.Id);
//         DeepAssert.Equal(entity, actualActivityEntity);
//     }
//     
//     [Fact]
//     public async Task RemoveActivity()
//     {
//         // Arrange
//         var entity = ActivitySeeds.Activity1;
//         
//         // Act
//         SchoolSystemDbContextSUT.Activities.Remove(entity);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();
//         
//         // Assert
//         await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//         var actualActivityEntity = await dbContext.Activities.FirstOrDefaultAsync(i => i.Id == entity.Id);
//         Assert.Null(actualActivityEntity);
//     }
//     
//     [Fact]
//     public async Task RetrieveDeletedActivityById()
//     {
//         // Arrange
//         var entity = ActivitySeeds.Activity1;
//         
//         // Act
//         SchoolSystemDbContextSUT.Activities.Remove(entity);
//         await SchoolSystemDbContextSUT.SaveChangesAsync();
//         var actualActivityEntity = await SchoolSystemDbContextSUT.Activities
//             .AsNoTracking()
//             .FirstOrDefaultAsync(i => i.Id == entity.Id);
//         
//         // Assert
//         Assert.Null(actualActivityEntity);
//     }
//
//     [Fact]
//     public async Task UpdateExisting_ActivityInformation_Successfully()
//     {
//         // Arrange
//         await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//         var activityToUpdate = await dbContext.Activities.FindAsync(ActivitySeeds.Activity1.Id);
//
//         var entity = new ActivityEntity
//         {
//             Name = "Test",
//             Id = activityToUpdate.Id, // Retain the original Id
//             Start = activityToUpdate.Start.AddHours(1),
//             End = activityToUpdate.End.AddHours(1),
//             Room = Room.D106,
//             Tag = 1,
//             Description = "Test of the activity entity updated"
//         };
//
//         // Act
//         activityToUpdate.Start = entity.Start;
//         activityToUpdate.End = entity.End;
//         activityToUpdate.Room = entity.Room;
//         activityToUpdate.Tag = entity.Tag;
//         activityToUpdate.Description = entity.Description;
//         await dbContext.SaveChangesAsync();
//         
//         // Assert
//         var updatedActivity = await dbContext.Activities.FindAsync(ActivitySeeds.Activity1.Id);
//         DeepAssert.Equal(entity, updatedActivity);
//
//     }
//     
//     [Fact]
//     public async Task GetActivityById()
//     {
//         // Arrange: Using a predefined seed
//         var entity = await SchoolSystemDbContextSUT.Activities.SingleAsync(i => i.Id == ActivitySeeds.Activity1.Id);
//         
//         // Act
//         var actualActivityEntity = await SchoolSystemDbContextSUT.Activities.FindAsync(entity.Id);
//         
//         // Assert
//         DeepAssert.Equal(entity, actualActivityEntity);
//     }
// }