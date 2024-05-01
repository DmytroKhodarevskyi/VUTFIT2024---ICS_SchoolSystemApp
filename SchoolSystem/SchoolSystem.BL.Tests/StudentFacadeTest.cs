using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Facades;
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
    public async Task Create_WithNonExistingItem_DoesNotThrowException()
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
    public async Task Seeded_DeleteById()
    {
        await _studentFacadeSUT.DeleteAsync(StudentSeeds.StudentActivityEntityDelete.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Students.AnyAsync(i => i.Id == StudentSeeds.StudentActivityEntityDelete.Id));
    }
    
    [Fact]
    public async Task Delete_StudentWithCourse_ThrowsEx()
    {
        //Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _studentFacadeSUT.DeleteAsync(Guid.Parse("00000000-8888-0001-0000-000000000008")));
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
    public async Task Seeded_InsertOrUpdate_StudentUpdated()
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
    public async Task GetStudentByNameSurname_Test()
    {
       var students = await _studentFacadeSUT.GetStudentByNameSurname("John", "Doe");
       
       Assert.Contains(StudentMapper.MapToListModel(StudentSeeds.Student1), students);
    }


    [Fact]
    public async Task GetStudentsByNameAsync_Test()
    {
        var results = await _studentFacadeSUT.GetStudentsByNameAsync(StudentSeeds.Student2.Name);

        // Assert
        Assert.NotNull(results);
        Assert.Equal(2, results.Count());
        Assert.Contains(results, s => s.Name == "Dima");
        Assert.Contains(results, s => s.Surname == "Trifanov");
    }
    
}