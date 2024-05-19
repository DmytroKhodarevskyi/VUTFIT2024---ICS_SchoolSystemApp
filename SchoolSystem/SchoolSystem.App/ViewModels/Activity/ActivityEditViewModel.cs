using CommunityToolkit.Mvvm.Input;
using DAL.Enums;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolSystem.App.ViewModels.Activity;

[QueryProperty(nameof(Activity), nameof(Activity))]
public partial class ActivityEditViewModel : ViewModelBase
{
    private readonly IActivityFacade activityFacade;
    private readonly ISubjectFacade subjectFacade;
    private readonly INavigationService navigationService;
    private readonly IAlertService alertService;

    public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;
    public List<Room> Rooms => Enum.GetValues(typeof(Room)).Cast<Room>().ToList();
    public TimeSpan TimeOfDayStart { get; set; }
    public TimeSpan TimeOfDayEnd { get; set; }
    public string[] Subjects { get; set; }

    private string selectedSubject;
    public string SelectedSubject
    {
        get => selectedSubject;
        set
        {
            SetProperty(ref selectedSubject, value);
            Activity.SubjectAbr = value;
        }
    }

    public ActivityEditViewModel(
        IActivityFacade activityFacade,
        ISubjectFacade subjectFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        this.activityFacade = activityFacade;
        this.subjectFacade = subjectFacade;
        this.navigationService = navigationService;
        this.alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        var subjectsEntities = await subjectFacade.GetAsync();
        Subjects = subjectsEntities.Select(s => s.Abbreviation).ToArray();
        SelectedSubject = Activity.SubjectAbr; 
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        Activity.Start = Activity.Start.Date + TimeOfDayStart;
        Activity.End = Activity.End.Date + TimeOfDayEnd;
        Activity.Subject = await subjectFacade.GetSubjectByAbbrAsync(Activity.SubjectAbr);
        Activity.SubjectId = Activity.Subject.Id;
        if (Activity.Start >= Activity.End)
        {
            await alertService.DisplayAsync("Invalid Time", "Activity cannot be added because start time is same or greater than end time.");
            return;
        }
        
        await activityFacade.SaveAsync(Activity);
        MessengerService.Send(new EditMessage { Id = Activity.Id });
        navigationService.SendBackButtonPressed();
    }

}
