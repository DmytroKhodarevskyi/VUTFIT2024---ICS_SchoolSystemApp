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
    INavigationService navigationService,
    IMessengerService messengerService)
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
        await activityFacade.SaveAsync(Activity);

        MessengerService.Send(new EditMessage { Id = Activity.Id });

        navigationService.SendBackButtonPressed();
    }
}