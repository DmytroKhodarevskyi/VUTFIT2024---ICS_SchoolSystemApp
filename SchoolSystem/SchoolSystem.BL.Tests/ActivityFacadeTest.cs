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
    public async Task CreateBaseActivity()
    {
        var model = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = "Math",
            Start = DateTime.Now,
            End = DateTime.Now,
            Tag = 1,
            SubjectId = ActivitySeeds.Activity1.SubjectId,  
        };

        var _ = await _activityFacadeSUT.SaveAsync(model);
    }
    
    [Fact]
    public async Task GetAll_Single_SeededActivity1()
    {
        var activities = await _activityFacadeSUT.GetAsync();

        Assert.Contains(ActivityMapper.MapToListModel(ActivitySeeds.Activity1), activities);
    }
    
    [Fact]
    public async Task GetById_SeededActivity1()
    {
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.Activity1.Id);

        Assert.Equal(ActivityMapper.MapToDetailModel(ActivitySeeds.Activity1), activity);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.EmptyActivity.Id);

        Assert.Null(activity);
    }
    
    [Fact]
    public async Task GetActivitiesBySubject()
    {
        var activities = await _activityFacadeSUT.GetAsyncFilter(Guid.Empty, null, null, 0, ActivitySeeds.Activity1.SubjectId);

        Assert.Contains(ActivityMapper.MapToListModel(ActivitySeeds.Activity1), activities);
    }
    
        
    
    [Fact]
    public async Task UpdateActivity()
    {
        var model = new ActivityDetailModel()
        {
            Id = ActivitySeeds.Activity1.Id,
            Name = "Math",
            Start = DateTime.Now,
            End = DateTime.Now,
            Tag = 1,
            SubjectId = ActivitySeeds.Activity1.SubjectId,  
        };

        var _ = await _activityFacadeSUT.SaveAsync(model);
    }

}