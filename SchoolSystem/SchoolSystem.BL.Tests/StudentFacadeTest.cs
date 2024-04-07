using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public sealed class StudentFacadeTests : CRUDFacadeTestsBase
{
    private readonly StudentFacade _studentFacadeSUT;
    
    public StudentFacadeTests(ITestOutputHelper output) : base(output)
    {
        _studentFacadeSUT = new StudentFacade(UnitOfWorkFactory, StudentMapper);
    }
    
    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new StudentDetailedModel()
        {
            Id = Guid.Empty,
            Name = @"John",
            Surname = @"Doe",
        };

        var _ = await _studentFacadeSUT.SaveAsync(model);
    }
    
    [Fact]
    public async Task GetAll_Single_SeededEskil()
    {
        var students = await _studentFacadeSUT.GetAsync();
        var student = students.Single(i => i.Id == StudentSeeds.Student1.Id);

        DeepAssert.Equal(StudentMapper.MapToListModel(StudentSeeds.Student1), student);
    }
    
    [Fact]
    public async Task GetById_SeededEskil()
    {
        var ingredient = await _studentFacadeSUT.GetAsync(StudentSeeds.Student1.Id);

        DeepAssert.Equal(StudentMapper.MapToDetailModel(StudentSeeds.Student1), ingredient);
    }
    
    [Fact]
    public async Task GetById_NonExistent()
    {
        var ingredient = await _studentFacadeSUT.GetAsync(StudentSeeds.EmptyStudentEntity.Id);

        Assert.Null(ingredient);
    }
    
    [Fact]
    public async Task SeededEskil_DeleteById_Deleted()
    {
        await _studentFacadeSUT.DeleteAsync(StudentSeeds.Student1.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Students.AnyAsync(i => i.Id == StudentSeeds.Student1.Id));
    }
    
    [Fact]
    public async Task Delete_StudentWithCourse_Throws()
    {
        //Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _studentFacadeSUT.DeleteAsync(StudentSeeds.StudentEvaluationEntityUpdate.Id));
    }
    
    [Fact]
    public async Task NewStudent_InsertOrUpdate_StudentAdded()
    {
        //Arrange
        var student = new StudentDetailedModel()
        {
            Id = Guid.Empty,
            Name = "John",
            Surname = "Doe",
        };

        //Act
        student = await _studentFacadeSUT.SaveAsync(student);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var studentFromDb = await dbxAssert.Students.SingleAsync(i => i.Id == student.Id);
        DeepAssert.Equal(student, StudentMapper.MapToDetailModel(studentFromDb));
    }
    
    [Fact]
    public async Task SeededEskil_InsertOrUpdate_StudentUpdated()
    {
        //Arrange
        var student = new StudentDetailedModel()
        {
            Id = StudentSeeds.StudentEvaluationEntityUpdate.Id,
            Name = StudentSeeds.StudentEvaluationEntityUpdate.Name,
            Surname = StudentSeeds.StudentEvaluationEntityUpdate.Surname,
        };
        student.Name += "updated";
        student.Surname += "updated";

        //Act
        await _studentFacadeSUT.SaveAsync(student);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var studentFromDb = await dbxAssert.Students.SingleAsync(i => i.Id == student.Id);
        DeepAssert.Equal(student, StudentMapper.MapToDetailModel(studentFromDb));
    }
    
    [Fact]
    public async Task GetStudentByNameSurname_ReturnsCorrectStudent()
    {
        // Arrange
        var student = new StudentDetailedModel()
        {
            Id = StudentSeeds.StudentEvaluationEntityUpdate.Id,
            Name = StudentSeeds.StudentEvaluationEntityUpdate.Name,
            Surname = StudentSeeds.StudentEvaluationEntityUpdate.Surname,
        };
       await _studentFacadeSUT.SaveAsync(student);

       var students = await _studentFacadeSUT.GetStudentByNameSurname("John", "Doe");
       
       DeepAssert.Contains(student, students);
    }


    [Fact]
    public async Task GetStudentsByNameAsync_ReturnsCorrectStudents()
    {
        var results = await _studentFacadeSUT.GetStudentsByNameAsync(StudentSeeds.Student2.Name);

        // Assert
        Assert.NotNull(results);
        Assert.Equal(2, results.Count());
        Assert.Contains(results, s => s.Name == "Dima");
        Assert.Contains(results, s => s.Surname == "Trifanov");
    }
}