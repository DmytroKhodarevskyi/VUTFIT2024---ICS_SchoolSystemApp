using System;
using System.Threading.Tasks;
<<<<<<< HEAD
using DAL.;
using DAL.Factories;
=======
using SchoolSystem.DAL;
using SchoolSystem.DAL.Factories;
>>>>>>> 9915cde2ad2526265306f995fab3881598c7546c
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
<<<<<<< HEAD
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);
=======
        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!, seedTestingData: true);
>>>>>>> 9915cde2ad2526265306f995fab3881598c7546c

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