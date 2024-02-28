using DAL.Enums;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Common.Tests.Seeds;

public static class SubjectSeeds
{
    public static readonly SubjectEntity EmptySubject = new()
    {
        Id = default,
        Name = default!,
        Abbreviation = default!,
    };
    
    public static readonly SubjectEntity IZP = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-0000000000089"),
        Name = "IZP",
        Abbreviation = "IZP",
    };
    
    public static readonly SubjectEntity IUS = new()
    {
        Id = Guid.Parse("00000000-0000-0000-0000-0000000000078"),
        Name = "IUS",
        Abbreviation = "IUS",
    };
    
    public static readonly SubjectEntity SubjectEntityWithNoStudAct = IZP with 
        { Id = Guid.Parse("00000000-0000-0000-0000-0000000000067"), 
            Students = Array.Empty<StudentEntity>(), 
            Activities = Array.Empty<ActivityEntity>()};
    
}
