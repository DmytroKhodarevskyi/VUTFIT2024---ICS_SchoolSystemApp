using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public sealed class StudentFacadeTest : CRUDFacadeTestsBase
{
    private readonly StudentFacade _studentFacadeSUT;

    public StudentFacadeTest(ITestOutputHelper output) : base(output)
    {
        _studentFacadeSUT = new StudentFacade(Mapper, UnitOfWorkFactory);
    }


    [Fact]
    public async Task GetStudentByNameSurname_ReturnsCorrectStudent()
    {
        // Arrange
        var expectedStudent = new StudentDetailedModel(
            Name: "John",
            Surname: "Doe",
            Photo: "photo.jpg"
        )
        {
            Id = Guid.Parse("0d4aa150-ad80-4d46-a511-4c888166e112"),
        }; //{ Name = "John", Surname = "Doe", Id = Guid.Parse("0d4fa150-ad80-4d46-a511-4c888166e112")};
       await _studentFacadeSUT.SaveAsync(expectedStudent);

       var students = await _studentFacadeSUT.GetStudentByNameSurname("John", "Doe");
       
       DeepAssert.Contains(expectedStudent, students);
    }


    [Fact]
    public async Task GetStudentsByNameAsync_ReturnsCorrectStudents()
    {
        // Arrange
        var students = new List<StudentDetailedModel>
        {
            new StudentDetailedModel ( "Emily", "Smith", "fwefe") { Id = Guid.Parse("0d2fa150-ad80-4d46-a511-4c888166e112") },
            new StudentDetailedModel ( "Emma", "Johnson", "fwefe") { Id = Guid.Parse("1d4fa150-ad80-4d46-a511-4c888166e112") }
        };

        students.ForEach(async (StudentDetailedModel x) => await _studentFacadeSUT.SaveAsync(x));
        var results = await _studentFacadeSUT.GetStudentsByNameAsync("Em");

        // Assert
        Assert.NotNull(results);
        Assert.Equal(2, results.Count());
        Assert.Contains(results, s => s.Name == "Emily");
        Assert.Contains(results, s => s.Name == "Emma");
    }
}