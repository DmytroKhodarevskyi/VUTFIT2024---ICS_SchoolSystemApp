using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Students;

[QueryProperty(nameof(StudentId), nameof(StudentId))]
public partial class SubjectAddViewModel(
    ISubjectFacade subjectFacade,
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public Guid StudentId { get; set; }

    public string[] Subjects { get; set; }

    public string SelectedSubject { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        var subjectsEntities = await subjectFacade.GetAsync();
        Subjects = subjectsEntities.Select(s => s.Name).ToArray();
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        var student = await studentFacade.GetAsync(StudentId);
        var subject = await subjectFacade.GetSubjectsByName(SelectedSubject);
        
        student.Subjects.Add(subject.FirstOrDefault());
        await studentFacade.SaveAsync(student);
        MessengerService.Send(new EditMessage { Id = StudentId });


        navigationService.SendBackButtonPressed();
    }
}