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
//
// namespace SchoolSystem.DAL.Tests
// {
//     public class DbContextSubjectTests : DbContextTestsBase
//     {
//         public DbContextSubjectTests(ITestOutputHelper output) : base(output)
//         {
//
//         }
//
//         [Fact]
//         public async Task AddNew_Subject_Persisted()
//         {
//             // Arrange
//             var subjectEntity = new SubjectEntity
//             {
//                 Name = "Math",
//                 Abbreviation = "MTH"
//             };
//
//             // Act
//             SchoolSystemDbContextSUT.Subjects.Add(subjectEntity);
//             await SchoolSystemDbContextSUT.SaveChangesAsync();
//
//             // Assert
//             await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//             var subjectFromDb = await dbContext.Subjects.FirstOrDefaultAsync(s => s.Name == "Math");
//             Assert.NotNull(subjectFromDb);
//             DeepAssert.Equal(subjectEntity, subjectFromDb);
//
//             SchoolSystemDbContextSUT.Subjects.Remove(subjectEntity);
//             await SchoolSystemDbContextSUT.SaveChangesAsync();
//         }
//         [Fact]
//         public async Task AddSubjectWithActivityAndStudent()
//         {
//             var subjectEntity = SubjectSeeds.EmptySubject with
//             {
//                 Name = "Database",
//                 Abbreviation = "IDS"
//             };
//
//             var activityEntity = ActivitySeeds.EmptyActivity with
//             {
//                 Start = new DateTime(2020, 03, 24, 03, 34, 50),
//                 End = new DateTime(2020, 03, 24, 08, 30, 50),
//                 Room = Room.D106,
//                 Tag = 1,
//                 Description = "Test"
//             };
//
//             var studentEntity = StudentSeeds.EmptyStudentEntity with
//             {
//                 Name = "Denys",
//                 Surname = "Chernenko"
//             };
//
//             // Add activity to the subject
//             subjectEntity.Activities.Add(activityEntity);
//
//             // Add student to the subject
//             subjectEntity.Students.Add(studentEntity);
//
//             // Act
//             SchoolSystemDbContextSUT.Subjects.Add(subjectEntity);
//             await SchoolSystemDbContextSUT.SaveChangesAsync();
//             
//             // Assert
//             await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//             var subjectFromDb = await dbContext.Subjects
//                 .Include(s => s.Activities)
//                 .Include(s => s.Students)
//                 .FirstOrDefaultAsync(s => s.Name == "Database");
//             Assert.NotNull(subjectFromDb);
//             DeepAssert.Equal(subjectEntity, subjectFromDb);
//             SchoolSystemDbContextSUT.Subjects.Remove(subjectEntity);
//             await SchoolSystemDbContextSUT.SaveChangesAsync();
//         }
//         
//         
//         [Fact]
//         public async Task UpdateExisting_SubjectInformation_Successfully()
//         {
//             // Arrange
//             await using var dbContext = await DbContextFactory.CreateDbContextAsync();
//             var subjectToUpdate = await dbContext.Subjects.FindAsync(SubjectSeeds.IZP.Id);
//
//             var entity = new SubjectEntity
//             {
//                 Id = subjectToUpdate.Id, // Retain the original Id
//                 Name = subjectToUpdate.Name + "Updated",
//                 Abbreviation = subjectToUpdate.Abbreviation + "U"
//             };
//
//             // Act
//             subjectToUpdate.Name = entity.Name;
//             subjectToUpdate.Abbreviation = entity.Abbreviation;
//             await dbContext.SaveChangesAsync();
//
//
//             // Assert
//             var updatedSubject = await dbContext.Subjects.FindAsync(SubjectSeeds.IZP.Id);
//             DeepAssert.Equal(entity, updatedSubject);
//         }
//         
//         [Fact]
//         public async Task GetById_IncludingActivitiesAndStudents_Subject()
//         {
//             // Act
//             var entity = await SchoolSystemDbContextSUT.Subjects
//                 .Include(s => s.Activities)
//                 .Include(s => s.Students)
//                 .SingleAsync(s => s.Id == SubjectSeeds.SubjectEntityUpdated.Id);
//
//             // Assert
//             DeepAssert.Equal(SubjectSeeds.SubjectEntityUpdated, entity, "Activities", "Students");
//         }
//         
//         [Fact]
//         public async Task GetById_Subject_SubjectRetrieved()
//         {
//             // Act
//             var subjectId = SubjectSeeds.IZP.Id;
//             var retrievedSubject = await SchoolSystemDbContextSUT.Subjects
//                 .Include(s => s.Activities)
//                 .SingleOrDefaultAsync(s => s.Id == subjectId);
//
//             // Assert
//             Assert.NotNull(retrievedSubject);
//             Assert.Equal(SubjectSeeds.IZP.Name, retrievedSubject.Name);
//             Assert.Equal(SubjectSeeds.IZP.Abbreviation, retrievedSubject.Abbreviation);
//             DeepAssert.Equal(SubjectSeeds.IZP with
//             {
//                 Students = Array.Empty<StudentEntity>(),
//                 Activities = Array.Empty<ActivityEntity>()
//             }, retrievedSubject);
//         }
//         
//         [Fact]
//         public async Task DeleteSubjectById_Successfully()
//         {
//             // Arrange
//             var subjectToDelete = SubjectSeeds.SubjectEntityDeleted;
//
//             // Act
//             SchoolSystemDbContextSUT.Subjects.Remove(
//                 await SchoolSystemDbContextSUT.Subjects.SingleAsync(i => i.Id == subjectToDelete.Id));
//                 await SchoolSystemDbContextSUT.SaveChangesAsync();
//
//             // Assert
//             Assert.False(await SchoolSystemDbContextSUT.Subjects.AnyAsync(s => s.Id == subjectToDelete.Id));
//         }
//         
//         [Fact]
//         public async Task Delete_Subject_SubjectDeleted()
//         {
//             // Arrange
//             var subjectToDelete = SubjectSeeds.SubjectEntityDeleted;
//             
//             // Act
//             SchoolSystemDbContextSUT.Subjects.Remove(subjectToDelete);
//             await SchoolSystemDbContextSUT.SaveChangesAsync();
//
//             // Assert
//             Assert.False(await SchoolSystemDbContextSUT.Subjects.AnyAsync(s => s.Id == subjectToDelete.Id));
//         }
//     }
// }
//         
