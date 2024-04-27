using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Subjects;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class SubjectDetailViewModel(
    ISubjectFacade subjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<EditMessage>
{
    public Guid Id { get; set; }
    public SubjectDetailedModel? Subject { get; private set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Subject = await subjectFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Subject is not null)
        {
            try
            {
                await subjectFacade.DeleteAsync(Subject.Id);
                MessengerService.Send(new DeleteMessage<StudentDetailedModel>());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Error", "Subject cannot be deleted.");
            }
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(SubjectEditViewModel.Subject)] = Subject });
    }

    public async void Receive(EditMessage message)
    {
        if (message.Id == Subject?.Id)
        {
            await LoadDataAsync();
        }
    }
}