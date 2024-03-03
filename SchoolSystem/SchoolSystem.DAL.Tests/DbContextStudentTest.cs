using System;
using System.Collections.Generic;
using DAL.Enums;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Seeds;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.DAL.Tests
{
    public class DbContextStudentTest : DbContextTestsBase
    {
        public DbContextStudentTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task AddNewStudentWithoutAnything()
        {
            var student = StudentSeeds.EmptyStudentEntity with
            {
                Name = "John",
                Surname = "Doe",
            };

            SchoolSystemDbContextSUT.Students.Add(student);
            await SchoolSystemDbContextSUT.SaveChangesAsync();

            await using var dbContext = await DbContextFactory.CreateDbContextAsync();
            var actualStudent = await dbContext.Students
                .SingleAsync(i => i.Id == student.Id);
            DeepAssert.Equal(student, actualStudent);
        }

        [Fact]
        public async Task AddStudentwithSubject()
        {
            var student = StudentSeeds.EmptyStudentEntity with
            {
                Name = "Denis",
                Surname = "Chernenko",
                
                Subjects = new List<SubjectEntity>
                {
                    SubjectSeeds.EmptySubject with
                    {
                        Name = "Math",
                        Abbreviation = "MTH",
                    }
                }
            };

            SchoolSystemDbContextSUT.Students.Add(student);
            await SchoolSystemDbContextSUT.SaveChangesAsync();

            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualStudent = await dbx.Students
                .Include(i=>i.Subjects)
                .SingleAsync(i => i.Id == student.Id);
                
            DeepAssert.Equal(student, actualStudent);
        }

        [Fact]
        public async Task AddStudent3()
        {
            var student = StudentSeeds.EmptyStudentEntity with
            {
                Name = "Dimon",
                Surname = "Trifonov",
               
            };

            SchoolSystemDbContextSUT.Students.Add(student);
            await SchoolSystemDbContextSUT.SaveChangesAsync();

            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualStudent = await dbx.Students
                .SingleAsync(i => i.Id == student.Id);
            DeepAssert.Equal(student, actualStudent);
        }

        
        [Fact]
        public async Task GetById()
        {
            var entity = await SchoolSystemDbContextSUT.Students
                .SingleAsync(i => i.Id == StudentSeeds.Student1.Id);

            DeepAssert.Equal(StudentSeeds.Student1, entity);
        }

        [Fact]
        public async Task UpdateStudent()
        {
            var baseEntity = StudentSeeds.StudentEntityUpdated;
            var newEntity = baseEntity with
            {
                Name = "Denis",
                Surname = "Chernenko",
            };
            SchoolSystemDbContextSUT.Students.Update(newEntity);
            await SchoolSystemDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Students.SingleAsync(i => i.Id == newEntity.Id);
            DeepAssert.Equal(newEntity, actualEntity);
        }

        [Fact]
        public async Task DeleteStudent()
        {
            var entity = StudentSeeds.StudentEntityDeleted;
            SchoolSystemDbContextSUT.Students.Remove(SchoolSystemDbContextSUT.Students.Single(i => i.Id == entity.Id));
            await SchoolSystemDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Students.SingleOrDefaultAsync(i => i.Id == entity.Id);
            Assert.Null(actualEntity);
        }
    }
}