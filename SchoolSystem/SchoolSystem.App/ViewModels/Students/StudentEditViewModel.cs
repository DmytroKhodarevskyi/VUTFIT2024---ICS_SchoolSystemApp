using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Students;

[QueryProperty(nameof(Student), nameof(Student))]
public partial class StudentEditViewModel(
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public StudentDetailedModel Student { get; init; } = StudentDetailedModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await studentFacade.SaveAsync(Student);

        MessengerService.Send(new EditMessage { Id = Student.Id });

        navigationService.SendBackButtonPressed();
    }
}