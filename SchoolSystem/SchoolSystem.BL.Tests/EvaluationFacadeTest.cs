using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public sealed class EvaluationFacadeTest : CRUDFacadeTestsBase
{
    private readonly EvaluationFacade _evaluationFacadeSUT;
    private readonly StudentFacade _studentFacadeSUT;
    private readonly SubjectFacade _subjectFacadeSUT;
    private readonly ActivityFacade _activityFacadeSUT;

    public EvaluationFacadeTest(ITestOutputHelper output) : base(output)
    {
        _evaluationFacadeSUT = new EvaluationFacade(Mapper, UnitOfWorkFactory);
        _studentFacadeSUT = new StudentFacade(Mapper, UnitOfWorkFactory);
        _subjectFacadeSUT = new SubjectFacade(Mapper, UnitOfWorkFactory);
        _activityFacadeSUT = new ActivityFacade(Mapper, UnitOfWorkFactory);
    }

    [Fact]
    public async Task GetFinalEvaluationOfStudentForSubject_ReturnsCorrectEvaluation()
    {
        // Arrange
        
        var subjectList = new SubjectListModel(Name: "Math", Abbreviation: "MTH")
        {
            Id = Guid.Parse("0d4fa150-ad01-4d46-a511-4c888166e112"),
        };
        var subjectDet = new SubjectDetailedModel(Name: "Math", Abbreviation: "MTH")
        {
            Id = Guid.Parse("0d4fa150-ad10-4d46-a511-4c888166e112"),
        };
        var student = new StudentDetailedModel(Name: "John", Surname: "Doe", Photo: "photo.jpg")
        {
           Id = Guid.Parse("0d4fa150-ad20-4d46-a511-4c888166e112"),
        };
       student.Subjects.Add(subjectList);
        var activity1 = new ActivityDetailModel(Start: DateTime.Now, End: DateTime.Now.AddDays(1), Name: "Test",
            Description: "Test", Room: Room.D104, Tag: 1)
        {
            Id = Guid.Parse("0d4fa150-ad30-4d46-a511-4c888166e112"),
        };
        activity1.Subject = subjectDet;
        // var activity2 = new ActivityDetailModel(Start: DateTime.Now, End: DateTime.Now.AddDays(1), Name: "Test",
        //     Description: "Test", Room: Room.D104, Tag: 1)
        // {
        //     Id = Guid.Parse("0d4fa150-ad40-4d46-a511-4c888166e112"),
        // };
        // await _activityFacadeSUT.SaveAsync(activity2);
        var evaluation1 = new EvaluationDetailModel(Score: 5, Description: "Test")
        {
            Id = Guid.Parse("0d4fa150-ad50-4d46-a511-4c888166e112"),
            
        };
        evaluation1.Student = student;
        evaluation1.Activity = activity1;
        await _evaluationFacadeSUT.SaveAsync(evaluation1);
        // var evaluation2 = new EvaluationDetailModel(Score: 10, Description: "Test")
        // {
        //     Id = Guid.Parse("0d4fa150-ad60-4d46-a511-4c888166e112"),
        //     Student = student,
        //     Activity = activity2
        // };
        // await _evaluationFacadeSUT.SaveAsync(evaluation2);
        var test = _evaluationFacadeSUT.GetAsync();
        var evaluation = await _evaluationFacadeSUT.GetFinalEvaluationOfStudentForSubject(student.Name, student.Surname, subjectList.Name);
        
        // Assert
        Assert.NotNull(evaluation);
        Assert.Equal(15, evaluation.Score);
    }
    
    [Fact]
    public async Task GetPassedScores_ReturnsCorrectEvaluations()
    {
        // Arrange
        var evaluation1 = new EvaluationDetailModel(Score: 5, Description: "Test");
        await _evaluationFacadeSUT.SaveAsync(evaluation1);
        var evaluation2 = new EvaluationDetailModel(Score: 10, Description: "Test");
        await _evaluationFacadeSUT.SaveAsync(evaluation2);
        var evaluation3 = new EvaluationDetailModel(Score: 15, Description: "Test");
        await _evaluationFacadeSUT.SaveAsync(evaluation3);
        
        // Act
        var evaluations = await _evaluationFacadeSUT.GetPassedScores(10);
        
        // Assert
        Assert.NotNull(evaluations);
        Assert.NotEmpty(evaluations);
        Assert.Equal(2, evaluations.Count);
    }

    [Fact]
    public async Task GetTopNEvaluations()
    {
        // Arrange
        var evaluation1 = new EvaluationDetailModel(Score: 5, Description: "Test")
        {
            Id = Guid.Parse("0d4fa150-ad80-4d46-a511-4c888166e112"),
        };
        await _evaluationFacadeSUT.SaveAsync(evaluation1);
        var evaluation2 = new EvaluationDetailModel(Score: 10, Description: "Test")
        {
            Id = Guid.Parse("0d4fa150-ad70-4d46-a511-4c888166e112"),
        };
        await _evaluationFacadeSUT.SaveAsync(evaluation2);
        var evaluation3 = new EvaluationDetailModel(Score: 15, Description: "Test")
        {
            Id = Guid.Parse("0d4fa150-ad60-4d46-a511-4c888166e112"),
        };
        await _evaluationFacadeSUT.SaveAsync(evaluation3);
        
        // Act
        var evaluations = await _evaluationFacadeSUT.GetTopScores(2);
        
        // Assert
        
        Assert.NotNull(evaluations);
        Assert.NotEmpty(evaluations);
        Assert.Equal(2, evaluations.Count);
        Assert.Equal(15, evaluations[0].Score);
        Assert.Equal(10, evaluations[1].Score);
        
    }
}