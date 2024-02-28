using System;
using System.Threading.Tasks;
using DAL.;
using DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

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