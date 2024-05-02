using CommunityToolkit.Mvvm.Input;
using DAL.Enums;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Activity;

[QueryProperty(nameof(Activity), nameof(Activity))]
public partial class ActivityEditViewModel(
    IActivityFacade activityFacade,
    ISubjectFacade subjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService)
{
    public ActivityDetailModel Activity{ get; init; } = ActivityDetailModel.Empty;

    public List<Room> Rooms => Enum.GetValues(typeof(Room)).Cast<Room>().ToList();
    
    public TimeSpan TimeOfDayStart { get; set; }
    public TimeSpan TimeOfDayEnd { get; set; }


    [RelayCommand]
    private async Task SaveAsync()
    {
        Activity.Start = Activity.Start.Date + TimeOfDayStart;
        Activity.End = Activity.End.Date + TimeOfDayEnd;
        var Subject = await subjectFacade.GetSubjectByAbbrAsync(Activity!.SubjectAbr);
        var SubjectId = Subject.Id;
        Activity.SubjectId = SubjectId;
        Activity.Subject = Subject;
        if (Activity.Start >= Activity.End)
        {
            await alertService.DisplayAsync("Invalid Time", "Activity cannot be add because start time is same or greater then end time.");
            return;
        }
        
        await activityFacade.SaveAsync(Activity);
        // await _subjectFacade.AddActivityToSubject(SubjectId, Activity.Id);
        MessengerService.Send(new EditMessage { Id = Activity.Id });
        navigationService.SendBackButtonPressed();
    }
    
}