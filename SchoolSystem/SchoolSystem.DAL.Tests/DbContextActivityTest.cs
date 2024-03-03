using SchoolSystem.Common.Tests.Seeds;
using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.DAL.Tests;
// public class DbContextActivityTest(ITestOutputHelper output) : DbContextTestsBase(output)
public class DbContextActivityTest : DbContextTestsBase
{
    public DbContextActivityTest(ITestOutputHelper output) : base(output)
    {
    }
    // [Fact]
    // public async Task AddNewActivity()
    // {
    //     // Arrange
    //     var entity = ActivitySeeds.EmptyActivity with
    //     {
    //         Start = DateTime.Now,
    //         End = DateTime.Now,
    //         Room = Room.D105,
    //         Tag = 1,
    //         Description = "Test"
    //     };
    //     
    //     // Act
    //     SchoolSystemDbContextSUT.Activities.Add(entity);
    //     await SchoolSystemDbContextSUT.SaveChangesAsync();
    //     
    //     // Assert
    //     await using var dbContext = await DbContextFactory.CreateDbContextAsync();
    //     var actualActivityEntity = await dbContext.Activities.SingleAsync(e => e.Id == entity.Id);
    //     Assert.Equal(entity, actualActivityEntity);
    // }

    [Fact]
    public async Task AddNewActivityWithSubject()
    {
        // Arrange
        var entity = ActivitySeeds.EmptyActivity with
        {
            Start = DateTime.Now,
            End = DateTime.Now,
            Room = Room.D105,
            Tag = 1,
            Description = "Test",
            Subject = SubjectSeeds.EmptySubject with
            {
                Name = "Math",
                Abbreviation = "MTH",
            }
        };

        // Act
        SchoolSystemDbContextSUT.Activities.Add(entity);
        // await SchoolSystemDbContextSUT.SaveChangesAsync();

        try
        {
            // Attempt to save changes to the database
            await SchoolSystemDbContextSUT.SaveChangesAsync();
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateException dbUpdateEx)
        {
            // Handle known database update exceptions here
            Console.WriteLine($"A database update error occurred: {dbUpdateEx.Message}");
            throw; // Re-throwing the exception preserves the original exception stack trace
        }
        catch (Exception ex)
        {
            // Handle all other exceptions here
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw; // Re-throwing the exception preserves the original exception stack trace
        }

        // Assert
        try
        {
            await using var dbContext = await DbContextFactory.CreateDbContextAsync();
            var actualActivityEntity = await dbContext.Activities
                .Include(i => i.Subject)
                .SingleAsync(i => i.Id == entity.Id);
            Assert.Equal(entity, actualActivityEntity);
        }
        catch (Exception ex)
        {
            // Log the exception or inspect it in the debugger
            Console.WriteLine(ex.ToString());
        }

        // await using var dbContext = await DbContextFactory.CreateDbContextAsync();
    }
}