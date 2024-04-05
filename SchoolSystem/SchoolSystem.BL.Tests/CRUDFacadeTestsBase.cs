using AutoMapper;
using AutoMapper.EquivalencyExpression;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolSystem.Common.Tests;
using SchoolSystem.DAL;
using Xunit;
using Xunit.Abstractions;
using Mapper = AutoMapper.Mapper;

namespace SchoolSystem.BL.Tests;

public class CRUDFacadeTestsBase: IAsyncLifetime
{
    protected CRUDFacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        //DbContextFactory = new DbContextSqLiteTestingFactory(GetType().FullName!, seedDALTestingData: true);
        var services = new ServiceCollection();
        string databaseName = "SchoolSystemDbContext"; 
        services.AddDbContextFactory<SchoolSystemDbContext>(options =>
            options.UseSqlite($"Data Source={databaseName};Cache=Shared"));
        var serviceProvider = services.BuildServiceProvider();

        DbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<SchoolSystemDbContext>>();

        var configuration = new MapperConfiguration(cfg =>
            {
                
                cfg.AddCollectionMappers();
                cfg.UseEntityFrameworkCoreModel<SchoolSystemDbContext>(serviceProvider);
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