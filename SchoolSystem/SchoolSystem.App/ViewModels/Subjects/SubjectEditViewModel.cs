using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Subjects;

[QueryProperty(nameof(Subject), nameof(Subject))]
public partial class SubjectEditViewModel(
    ISubjectFacade subjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public SubjectDetailedModel Subject { get; init; } = SubjectDetailedModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await subjectFacade.SaveAsync(Subject);

        MessengerService.Send(new EditMessage { Id = Subject.Id });

        navigationService.SendBackButtonPressed();
    }
}