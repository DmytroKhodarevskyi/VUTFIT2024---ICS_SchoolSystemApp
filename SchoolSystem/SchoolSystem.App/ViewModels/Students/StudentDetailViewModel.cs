using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Students;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class StudentDetailViewModel(
    IStudentFacade studentFacade,
    ISubjectFacade subjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<EditMessage>
{
    public Guid Id { get; set; }
    public StudentDetailedModel? Student { get; private set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Student = await studentFacade.GetAsync(Id);
        
        Student.Subjects = new ObservableCollection<SubjectListModel>(await subjectFacade.GetSubjectsByName(Student.Name));
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Student is not null)
        {
            try
            {
                await studentFacade.DeleteAsync(Student.Id);
                MessengerService.Send(new DeleteMessage<StudentListModel>());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Error", "Student cannot be deleted.");
            }
        }
    }

    [RelayCommand]
    private async Task GoToAddSubjectAsync()
    {
        await navigationService.GoToAsync("/addSubject",
            new Dictionary<string, object?> { [nameof(SubjectAddViewModel.StudentId)] = Id });
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(StudentEditViewModel.Student)] = Student });
    }

    public async void Receive(EditMessage message)
    {
        if (message.Id == Student?.Id)
        {
            await LoadDataAsync();
        }
    }
}