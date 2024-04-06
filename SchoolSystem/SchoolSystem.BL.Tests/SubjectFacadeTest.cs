using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;



namespace SchoolSystem.BL.Tests;

public sealed class SubjectFacadeTest : CRUDFacadeTestsBase
{
    private readonly SubjectFacade _subjectFacadeSUT;

    public SubjectFacadeTest(ITestOutputHelper output) : base(output)
    {
        _subjectFacadeSUT = new SubjectFacade(Mapper, UnitOfWorkFactory);
    }

    [Fact]
    public async Task GetSubjectByName_ReturnsCorrectSubject()
    {
        // Arrange
        var expectedSubject = new SubjectDetailedModel("Mathematics", "Math");

        // Save the expected subject to the database
        await _subjectFacadeSUT.SaveAsync(expectedSubject);

        // Act
        var subject = await _subjectFacadeSUT.GetSubjectByName("Mathematics");

        // Assert
        Assert.NotNull(subject);
        Assert.Equal(expectedSubject.Name, subject.Name);
        Assert.Equal(expectedSubject.Abbreviation, subject.Abbreviation);
    }
    
    [Fact]
    public async Task GetSubjectByAbbr_ReturnsCorrectSubject()
    {
        // Arrange
        var expectedSubject = new SubjectDetailedModel("Mathematics", "Math");

        // Save the expected subject to the database
        await _subjectFacadeSUT.SaveAsync(expectedSubject);

        // Act
        var subject = await _subjectFacadeSUT.GetSubjectByAbbr("Math");

        // Assert
        Assert.NotNull(subject);
        Assert.Equal(expectedSubject.Name, subject.Name);
        Assert.Equal(expectedSubject.Abbreviation, subject.Abbreviation);
    }
    
    [Fact]
    public async Task GetSubjectsByNameAsync_ReturnsCorrectSubjects()
    {
        // Arrange
        var subjects = new List<SubjectDetailedModel>
        {
            new SubjectDetailedModel("Mathematics", "Math") {Id = Guid.NewGuid()},
            new SubjectDetailedModel("Mathematics", "Math") {Id = Guid.NewGuid()}
        };
        
        // Save the expected subjects to the database
        subjects.ForEach(async (SubjectDetailedModel s) => await _subjectFacadeSUT.SaveAsync(s).ConfigureAwait(false));

        // Act
        var results = await _subjectFacadeSUT.GetSubjectsByNameAsync("Mathematics");

        // Assert
        Assert.NotNull(results);
        Assert.Equal(2, results.Count());
        Assert.Contains(results, s => s.Name == "Mathematics");
    }
    
    
    [Fact]
    public async Task GetSubjectsByAbbrAsync_ReturnsCorrectSubjects()
    {
        // Arrange
        var abbreviation = "Math";
        var expectedSubjects = new List<SubjectDetailedModel>
        {
            new SubjectDetailedModel("Mathematics", "Math"),
            new SubjectDetailedModel("Mathematics", "Math")
        };

        // Save the expected subjects to the database
        var subjectFacade = new SubjectFacade(Mapper, UnitOfWorkFactory);
        foreach (var subject in expectedSubjects)
        {
            await subjectFacade.SaveAsync(subject);
        }

        // Act
        var results = await subjectFacade.GetSubjectsByAbbrAsync(abbreviation);

        // Assert
        Assert.NotNull(results);
        Assert.Equal(2, results.Count());
        Assert.All(results, s => Assert.Equal("Math", s.Abbreviation));
    }

    
    
    
    
    
    
    // [Fact]
    // public async Task GetStudentsByNameSubject_ReturnsCorrectStudents()
    // {
    //     // Arrange
    //     var expectedSubjectName = "Mathematics";
    //     var expectedStudents = new List<StudentDetailedModel>
    //     {
    //         new StudentDetailedModel("John", "Doe", null),
    //         new StudentDetailedModel("Emily", "Smith", null)
    //     };
    //
    //
    //     var studentFacade = new StudentFacade(Mapper, UnitOfWorkFactory);
    //     foreach (var student in expectedStudents)
    //     {
    //         await studentFacade.SaveAsync(student);
    //     }
    //
    //     // Initialize your SubjectFacade
    //     var subjectFacade = new SubjectFacade(Mapper, UnitOfWorkFactory);
    //
    //     // Act
    //     var students = await subjectFacade.GetStudentsByNameSubject(expectedSubjectName);
    //
    //     // Assert
    //     Assert.NotNull(students);
    //     Assert.Equal(expectedStudents.Count, students.Count());
    //
    //     foreach (var expectedStudent in expectedStudents)
    //     {
    //         Assert.Contains(students, s => s.Name == expectedStudent.Name && s.Surname == expectedStudent.Surname);
    //     }
    // }
    //
    // [Fact]
    // public async Task GetStudentsByAbbrSubject_ReturnsCorrectStudents()
    // {
    //     // Arrange
    //     var expectedSubjectAbbreviation = "Math";
    //     var expectedStudents = new List<StudentDetailedModel>
    //     {
    //         new StudentDetailedModel("John", "Doe", null),
    //         new StudentDetailedModel("Emily", "Smith", null)
    //     };
    //
    //     // Save the expected students to the database
    //     var studentFacade = new StudentFacade(Mapper, UnitOfWorkFactory);
    //     foreach (var student in expectedStudents)
    //     {
    //         await studentFacade.SaveAsync(student);
    //     }
    //
    //     // Initialize your SubjectFacade
    //     var subjectFacade = new SubjectFacade(Mapper, UnitOfWorkFactory);
    //
    //     // Act
    //     var students = await subjectFacade.GetStudentsByAbbrSubject(expectedSubjectAbbreviation);
    //
    //     // Assert
    //     Assert.NotNull(students);
    //     Assert.Equal(expectedStudents.Count, students.Count());
    //
    //     foreach (var expectedStudent in expectedStudents)
    //     {
    //         Assert.Contains(students, s => s.Name == expectedStudent.Name && s.Surname == expectedStudent.Surname);
    //     }
    // }
}
