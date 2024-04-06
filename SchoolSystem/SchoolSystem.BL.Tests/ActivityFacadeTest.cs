using DAL.Enums;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public sealed class ActivityFacadeTest : CRUDFacadeTestsBase
{
    private readonly ActivityFacade _activityFacadeSUT;

    public ActivityFacadeTest(ITestOutputHelper output) : base(output)
    {
        _activityFacadeSUT = new ActivityFacade(Mapper, UnitOfWorkFactory);
    }


    [Fact]
    public async Task GetRoomActivities_ReturnsCorrectListActivity()
    {
        var activities = new List<ActivityDetailModel>
        {
            new ActivityDetailModel(
                new DateTime(2024, 6, 4, 15, 0, 0), 
                new DateTime(2024, 6, 4, 17, 0, 0), 
                Room.D104,
                1,
                "Desc for first activity"
                ) 
            { Id = Guid.Parse("0d2fa150-ad80-4d46-a511-4c877166e112") },
            new ActivityDetailModel(
                new DateTime(2024, 7, 4, 15, 0, 0), 
                new DateTime(2024, 7, 4, 17, 0, 0), 
                Room.D105,
                2,
                "Desc for second activity"
            ) 
            { Id = Guid.Parse("1d4fa150-ad80-4d46-a511-4c866166e112") }
        };
        
        activities.ForEach(async (ActivityDetailModel s) => await _activityFacadeSUT.SaveAsync(s).ConfigureAwait(false));

        // var results = await _activityFacadeSUT.GetRoomActivities(Room.D105);
        
        var res_d105 = await _activityFacadeSUT.GetRoomActivities(Room.D105);

        // Assert
        Assert.NotNull(res_d105);
        Assert.Single(res_d105);
        Assert.Contains(res_d105, s => s.Room == Room.D105);
        Assert.Contains(res_d105, s => s.Tag == 2);
        
        var res_d104 = await _activityFacadeSUT.GetRoomActivities(Room.D104);

        // Assert
        Assert.NotNull(res_d104);
        Assert.Single(res_d104);
        Assert.Contains(res_d104, s => s.Room == Room.D104);
        Assert.Contains(res_d104, s => s.Tag == 1);
        // Assert.Contains(results, s => s.Name == "Emma");
    }


    [Fact]
    public async Task GetActivitiesAfter_ReturnsCorrectListActivity()
    {
        var activities = new List<ActivityDetailModel>
        {
            new ActivityDetailModel(
                    new DateTime(2024, 6, 4, 15, 0, 0), 
                    new DateTime(2024, 6, 4, 17, 0, 0), 
                    Room.D104,
                    1,
                    "Desc for first activity"
                ) 
                { Id = Guid.Parse("0d2fa150-ad80-4d46-a511-4c877166e112") },
            new ActivityDetailModel(
                    new DateTime(2024, 7, 4, 15, 0, 0), 
                    new DateTime(2024, 7, 4, 17, 0, 0), 
                    Room.D105,
                    2,
                    "Desc for second activity"
                ) 
                { Id = Guid.Parse("1d4fa150-ad80-4d46-a511-4c866166e112") }
        };
        
        activities.ForEach(async (ActivityDetailModel s) => await _activityFacadeSUT.SaveAsync(s).ConfigureAwait(false));

        var res_after2023 = await _activityFacadeSUT.GetActivitiesAfter(new DateTime(2023, 7, 4, 17, 0, 0));

        // Assert
        Assert.NotNull(res_after2023);
        Assert.Equal(2, res_after2023.Count());
        Assert.Contains(res_after2023, s => s.Tag == 1);
        Assert.Contains(res_after2023, s => s.Tag == 2);
        
        var res_after2025 = await _activityFacadeSUT.GetActivitiesAfter(new DateTime(2025, 7, 4, 17, 0, 0));

        // Assert
        Assert.NotNull(res_after2025);
        Assert.Empty(res_after2025);
    }
    
    [Fact]
    public async Task GetActivitieBefore_ReturnsCorrectListActivity()
    {
        var activities = new List<ActivityDetailModel>
        {
            new ActivityDetailModel(
                    new DateTime(2024, 6, 4, 15, 0, 0), 
                    new DateTime(2024, 6, 4, 17, 0, 0), 
                    Room.D104,
                    1,
                    "Desc for first activity"
                ) 
                { Id = Guid.Parse("0d2fa150-ad80-4d46-a511-4c877166e112") },
            new ActivityDetailModel(
                    new DateTime(2024, 7, 4, 15, 0, 0), 
                    new DateTime(2024, 7, 4, 17, 0, 0), 
                    Room.D105,
                    2,
                    "Desc for second activity"
                ) 
                { Id = Guid.Parse("1d4fa150-ad80-4d46-a511-4c866166e112") }
        };
        
        activities.ForEach(async (ActivityDetailModel s) => await _activityFacadeSUT.SaveAsync(s).ConfigureAwait(false));

        var res_before2025 = await _activityFacadeSUT.GetActivitiesBefore(new DateTime(2025, 7, 4, 17, 0, 0));

        // Assert
        Assert.NotNull(res_before2025);
        Assert.Equal(2, res_before2025.Count());
        Assert.Contains(res_before2025, s => s.Tag == 1);
        Assert.Contains(res_before2025, s => s.Tag == 2);
        
        var res_before2023 = await _activityFacadeSUT.GetActivitiesBefore(new DateTime(2023, 7, 4, 17, 0, 0));

        // Assert
        Assert.NotNull(res_before2023);
        Assert.Empty(res_before2023);
    }
    
    [Fact]
    public async Task GetActivitiesByTag_ReturnsCorrectListActivity()
    {
        var activities = new List<ActivityDetailModel>
        {
            new ActivityDetailModel(
                    new DateTime(2024, 6, 4, 15, 0, 0), 
                    new DateTime(2024, 6, 4, 17, 0, 0), 
                    Room.D104,
                    2,
                    "Desc for first activity"
                ) 
                { Id = Guid.Parse("0d2fa150-ad80-4d46-a511-4c877166e112") },
            new ActivityDetailModel(
                    new DateTime(2024, 7, 4, 15, 0, 0), 
                    new DateTime(2024, 7, 4, 17, 0, 0), 
                    Room.D105,
                    2,
                    "Desc for second activity"
                ) 
                { Id = Guid.Parse("1d4fa150-ad80-4d46-a511-4c866166e112") }
        };
        
        activities.ForEach(async (ActivityDetailModel s) => await _activityFacadeSUT.SaveAsync(s).ConfigureAwait(false));

        var res_filled = await _activityFacadeSUT.GetActivitiesByTag(2);

        // Assert
        Assert.NotNull(res_filled);
        Assert.Equal(2, res_filled.Count());
        Assert.Contains(res_filled, s => s.Tag == 2);
        
        var res_empty = await _activityFacadeSUT.GetActivitiesByTag(0);

        // Assert
        Assert.NotNull(res_empty);
        Assert.Empty(res_empty);
    }
}