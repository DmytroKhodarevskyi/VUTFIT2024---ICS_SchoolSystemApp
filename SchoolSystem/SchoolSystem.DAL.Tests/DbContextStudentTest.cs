// using System;
// using System.Collections.Generic;
// using SchoolSystem.Common.Tests;
// using SchoolSystem.Common.Tests.Seeds;
// using DAL.Entities;
// using Microsoft.EntityFrameworkCore;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace SchoolSystem.DAL.Tests
// {
//     public class DbContextStudentTest : DbContextTestsBase
//     {
//         public DbContextStudentTest(ITestOutputHelper output) : base(output)
//         {
//         }
//
//         [Fact]
//         public async Task AddNewStudentWithoutAnything()
//         {
//             var student = StudentSeeds.EmptyStudentEntity with
//             {
//                 Name = "John",
//                 Surname = "Doe",
//             };
//
//             SchoolSystemDbContextSUT.Students.Add(student);
//             await SchoolSystemDbContextSUT.SaveChangesAsync();
//
//             await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//             var actualStudent = await dbContext.Students
//                 .SingleAsync(i => i.Id == student.Id);
//             DeepAssert.Equal(student, actualStudent);
//         }
//
//         [Fact]
//         public async Task AddStudentWithActivity()
//         {
//             var student = StudentSeeds.EmptyStudentEntity with
//             {
//                 Name = "Denis",
//                 Surname = "Chernenko",
//                 Activities = new List<ActivityEntity>
//                 {
//                     ActivitySeeds.Activity1, ActivitySeeds.Activity2
//                 }
//             };
//
//             SchoolSystemDbContextSUT.Students.Add(student);
//             await SchoolSystemDbContextSUT.SaveChangesAsync();
//
//             await using var dbx = await DbContextFactory.CreateDbContextAsync();
//             var actualStudent = await dbx.Students
//                 .Include(i => i.Activities)
//                 .SingleAsync(i => i.Id == student.Id);
//             DeepAssert.Equal(student, actualStudent);
//         }
//
//         [Fact]
//         public async Task AddStudentWithActivitiesAndEvalautions()
//         {
//             var student = StudentSeeds.EmptyStudentEntity with
//             {
//                 Name = "Dimon",
//                 Surname = "Trifonov",
//                 Activities = new List<ActivityEntity>
//                 {
//                     ActivitySeeds.Activity1
//                 },
//                 Evaluations = new List<EvaluationEntity>
//                 {
//                     EvaluationSeeds.Evaluation1
//                 }
//             };
//
//             SchoolSystemDbContextSUT.Students.Add(student);
//             await SchoolSystemDbContextSUT.SaveChangesAsync();
//
//             await using var dbx = await DbContextFactory.CreateDbContextAsync();
//             var actualStudent = await dbx.Students
//                 .Include(i => i.Activities)
//                 .SingleAsync(i => i.Id == student.Id);
//             DeepAssert.Equal(student, actualStudent);
//         }
//
//         [Fact]
//         public async Task GetStudentById()
//         {
//             var entity = await SchoolSystemDbContextSUT.Students.SingleAsync(i => i.Id == StudentSeeds.StudentEntityWithNoSubjActivEval.Id);
//
//             Assert.Equal(StudentSeeds.StudentEntityWithNoSubjActivEval, entity);
//         }
//         [Fact]
//         public async Task GetById_IncludingActivity()
//         {
//             var entity = await SchoolSystemDbContextSUT.Students
//                 .Include(i => i.Activities)
//                 .SingleAsync(i => i.Id == StudentSeeds.Student1.Id);
//
//             DeepAssert.Equal(StudentSeeds.Student1, entity);
//         }
//
//         [Fact]
//         public async Task UpdateStudent()
//         {
//             var baseEntity = StudentSeeds.StudentEntityUpdated;
//             var newEntity = baseEntity with
//             {
//                 Name = "Denis",
//                 Surname = "Chernenko",
//             };
//             SchoolSystemDbContextSUT.Students.Update(newEntity);
//             await SchoolSystemDbContextSUT.SaveChangesAsync();
//
//             //Assert
//             await using var dbx = await DbContextFactory.CreateDbContextAsync();
//             var actualEntity = await dbx.Students.SingleAsync(i => i.Id == newEntity.Id);
//             DeepAssert.Equal(newEntity, actualEntity);
//         }
//
//         [Fact]
//         public async Task DeleteStudent()
//         {
//             var entity = StudentSeeds.StudentEntityDeleted;
//             SchoolSystemDbContextSUT.Students.Remove(SchoolSystemDbContextSUT.Students.Single(i => i.Id == entity.Id));
//             await SchoolSystemDbContextSUT.SaveChangesAsync();
//
//             //Assert
//             await using var dbx = await DbContextFactory.CreateDbContextAsync();
//             var actualEntity = await dbx.Students.SingleOrDefaultAsync(i => i.Id == entity.Id);
//             Assert.Null(actualEntity);
//         }
//     }
// }