using DAL.Enums;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public sealed class ActivityFacadeTests : CRUDFacadeTestsBase
{
    private readonly ActivityFacade _activityFacadeSUT;

    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityMapper);
    }
    
    [Fact]
    public async Task GetAll_Single_SeededActivity1()
    {
        // Act
        var activities = await _activityFacadeSUT.GetAsync();

        // Assert
        var expectedActivity = ActivityMapper.MapToListModel(ActivitySeeds.Activity1);
        Assert.Contains(activities, activity => activity.Id == expectedActivity.Id);
    }
    
    [Fact]
    public async Task GetById_SeededActivity1()
    {
        // Act
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.Activity1.Id);

        // Assert
        var expectedActivity = ActivityMapper.MapToDetailModel(ActivitySeeds.Activity1);
        Assert.Equal(expectedActivity.Id, activity.Id);
        Assert.Equal(expectedActivity.Name, activity.Name);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        // Act
        var activity = await _activityFacadeSUT.GetAsync(Guid.NewGuid());  // Use a new GUID to simulate non-existence

        // Assert
        Assert.Null(activity);
    }
        
    
    [Fact]
    public async Task GetAsyncFilterTag_ReturnsCorrectActivities()
    {
        var result = await _activityFacadeSUT.GetAsyncFilterTag(1);
        Assert.Single(result);
        Assert.Equal("IZP", result.First().Name);
    }

    [Fact]
    public async Task GetAsyncListBySubject_ReturnsActivitiesForSubject()
    {
        var result = await _activityFacadeSUT.GetAsyncListBySubject(ActivitySeeds.Activity1.SubjectId);
        Assert.Single(result);
        Assert.Equal("IZP", result.First().Name);
    }

    [Fact]
    public async Task GetActivityByName_ReturnsCorrectActivity()
    {
        var result = await _activityFacadeSUT.GetActivityByName("IZP");
        Assert.NotNull(result);
        Assert.Equal("IZP", result.Name);
    }

}