using System;
using System.Threading.Tasks;
using CarPool.Common.Tests;
using CarPool.Common.Tests.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
	protected DbContextTestsBase(ITestOutputHelper output)
	{
		XUnitTestOutputConverter converter = new(output);
		Console.SetOut(converter);

		// DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
		// DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
		DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

		CarPoolDbContextSUT = DbContextFactory.CreateDbContext();
	}

	protected IDbContextFactory<CarPoolDbContext> DbContextFactory { get; }
	protected CarPoolDbContext CarPoolDbContextSUT { get; }


	public async Task InitializeAsync()
	{
		await CarPoolDbContextSUT.Database.EnsureDeletedAsync();
		await CarPoolDbContextSUT.Database.EnsureCreatedAsync();
	}

	public async Task DisposeAsync()
	{
		await CarPoolDbContextSUT.Database.EnsureDeletedAsync();
		await CarPoolDbContextSUT.DisposeAsync();
	}
}