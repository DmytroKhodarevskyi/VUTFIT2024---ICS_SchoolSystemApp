
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SchoolSystem.BL.Models;
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

        DbContextFactory = new SchoolSystemDbContextSqLiteTestingFactory(GetType().FullName!, seedTestingData: true);
        // var services = new ServiceCollection();
        // string databaseName = "SchoolSystemDbContext"; 
        // services.AddDbContextFactory<SchoolSystemDbContext>(options =>
        //     options.UseSqlite($"Data Source={databaseName};Cache=Shared"));
        // var serviceProvider = services.BuildServiceProvider();

        //DbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<SchoolSystemDbContext>>()

        var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StudentDetailedModel.MapperProfile>();
                cfg.AddProfile<StudentListModel.MapperProfile>();
                cfg.AddProfile<EvaluationDetailModel.MapperProfile>();
                cfg.AddProfile<EvaluationListModel.MapperProfile>();
                cfg.AddProfile<SubjectDetailedModel.MapperProfile>();
                cfg.AddProfile<SubjectListModel.MapperProfile>();
                cfg.AddProfile<ActivityDetailModel.MapperProfile>();
                cfg.AddProfile<ActivityListModel.MapperProfile>();
                cfg.AddCollectionMappers();
                var serviceProvider = DbContextFactory.CreateDbContext();
                cfg.UseEntityFrameworkCoreModel(serviceProvider.Model);
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