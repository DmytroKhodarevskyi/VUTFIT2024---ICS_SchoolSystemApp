using AutoMapper;
using AutoMapper.EquivalencyExpression;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Factories;
using SchoolSystem.DAL;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public class CRUDFacadeTestsBase: IAsyncLifetime
{
    protected CRUDFacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        //DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedDALTestingData: true);
        DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);


        var configuration = new MapperConfiguration(cfg =>
            {
                
                cfg.AddCollectionMappers();

                using var dbContext = DbContextFactory.CreateDbContext();
                cfg.UseEntityFrameworkCoreModel<SchoolSystemDbContext>();
            }
        );
        Mapper = new Mapper(configuration);
        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory, Mapper);

        Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    protected IDbContextFactory<SchoolSystemDbContext> DbContextFactory { get; }
    protected Mapper Mapper { get; }
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}