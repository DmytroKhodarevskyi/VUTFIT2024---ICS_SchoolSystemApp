using System;
using System.Threading.Tasks;
using SchoolSystem.DAL;
using SchoolSystem.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!, seedTestingData: true);
        SchoolSystemDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<SchoolSystemDbContext> DbContextFactory { get; }
    protected SchoolSystemDbContext SchoolSystemDbContextSUT { get; }


    public async Task InitializeAsync()
    {
        await SchoolSystemDbContextSUT.Database.EnsureDeletedAsync();
        await SchoolSystemDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await SchoolSystemDbContextSUT.Database.EnsureDeletedAsync();
        await SchoolSystemDbContextSUT.DisposeAsync();
    }
}