using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.BL.Mappers;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Factories;
using SchoolSystem.DAL;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public class CRUDFacadeTestsBase : IAsyncLifetime
{
    protected CRUDFacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);
        
        var dbname = GetType().FullName!;
        
        DbContextFactory = new SchoolSystemDbContextSqLiteTestingFactory(dbname, seedTestingData: true);
        
        ActivityMapper = new ActivityModelMapper();
        EvaluationMapper = new EvaluationModelMapper();
        SubjectMapper = new SubjectModelMapper();
        StudentSubjectMapper = new StudentSubjectModelMapper();
        StudentMapper = new StudentModelMapper(StudentSubjectMapper);
        
        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<SchoolSystemDbContext> DbContextFactory { get; }
    
    protected StudentSubjectModelMapper StudentSubjectMapper { get; }

    protected StudentModelMapper StudentMapper { get; }
    protected SubjectModelMapper SubjectMapper { get; }
    
    protected ActivityModelMapper ActivityMapper { get; }

    protected EvaluationModelMapper EvaluationMapper { get; }
    
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