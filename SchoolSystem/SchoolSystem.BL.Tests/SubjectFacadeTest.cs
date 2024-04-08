using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public sealed class SubjectFacadeTests : CRUDFacadeTestsBase
{
    private readonly SubjectFacade _subjectFacadeSUT;

    public SubjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _subjectFacadeSUT = new SubjectFacade(UnitOfWorkFactory, SubjectMapper);
    }
    
    [Fact]
    public async Task CreateBaseSubject()
    {
        var model = new SubjectDetailedModel()
        {
            Id = Guid.Empty,
            Name = "Math",
            Abbreviation = "MTH",
        };

        var _ = await _subjectFacadeSUT.SaveAsync(model);
    }
    
    [Fact]
    public async Task GetAll_Single_SeededSubject1()
    {
        var subjects = await _subjectFacadeSUT.GetAsync();
        var subject = subjects.Single(i => i.Id == SubjectSeeds.IUS.Id);

        DeepAssert.Equal(SubjectMapper.MapToListModel(SubjectSeeds.IUS), subject);
    }
    
    [Fact]
    public async Task GetSubjectById()
    {
        var subject = await _subjectFacadeSUT.GetAsync(SubjectSeeds.IUS.Id);

        DeepAssert.Equal(SubjectMapper.MapToDetailModel(SubjectSeeds.IUS), subject);
    }
    
    [Fact]
    public async Task GetSubjectByAbbr()
    {
        var subjects = await _subjectFacadeSUT.GetSubjectsByAbrAsync(SubjectSeeds.IUS.Abbreviation);

        DeepAssert.Contains(SubjectMapper.MapToListModel(SubjectSeeds.IUS), subjects);
    }
    
    [Fact]
    public async Task GetSubjectByName()
    {
        var subjects = await _subjectFacadeSUT.GetSubjectByName(SubjectSeeds.IUS.Name);

        DeepAssert.Contains(SubjectMapper.MapToListModel(SubjectSeeds.IUS), subjects);
    }
    
    [Fact]
    public async Task GetSubjectsByNameAsync()
    {
        var subjects = await _subjectFacadeSUT.GetSubjectsByNameAsync(SubjectSeeds.IUS.Name);

        var subject = subjects.Single(i => i.Id == SubjectSeeds.IUS.Id);
        
        DeepAssert.Equal(SubjectMapper.MapToListModel(SubjectSeeds.IUS), subject);
    }
    
    [Fact]
    public async Task GetSubjectsByAbbrAsync()
    {
        var subjects = await _subjectFacadeSUT.GetSubjectsByAbbrAsync(SubjectSeeds.IUS.Abbreviation);

        var subject = subjects.Single(i => i.Id == SubjectSeeds.IUS.Id);
        
        DeepAssert.Equal(SubjectMapper.MapToListModel(SubjectSeeds.IUS), subject);
    }
    
    [Fact]
    public async Task Update_ExistingItem()
    {
        var model = new SubjectDetailedModel()
        {
            Id = SubjectSeeds.IUS.Id,
            Name = "Math",
            Abbreviation = "MTH",
        };

        var _ = await _subjectFacadeSUT.SaveAsync(model);
        
        var subject = await _subjectFacadeSUT.GetAsync(SubjectSeeds.IUS.Id);
        
        DeepAssert.Equal(model, subject);
    }
    
    [Fact]
    public async Task Update_NonExistentItem()
    {
        var model = new SubjectDetailedModel()
        {
            Id = Guid.NewGuid(),
            Name = "Math",
            Abbreviation = "MTH",
        };

        var _ = await _subjectFacadeSUT.SaveAsync(model);
        
        var subject = await _subjectFacadeSUT.GetAsync(model.Id);
        
        DeepAssert.Equal(model, subject);
    }
    
    

}