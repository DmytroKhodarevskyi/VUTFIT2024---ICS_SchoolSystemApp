using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Evaluations;

public partial class EvaluationEditViewModel(
    IEvaluationFacade evaluationFacade,
    IActivityFacade activityFacade,
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService)
{
    public EvaluationDetailModel Evaluation { get; init; } = EvaluationDetailModel.Empty;
    
    public string[] Activities { get; set; }
    public string[] Students { get; set; }

    public string SelectedStudent { get; set; }
    public string SelectedActivity { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        var activitiesEntities = await activityFacade.GetAsync();
        Activities = activitiesEntities.Select(a => a.Name).ToArray();
        var studentsEntities = await studentFacade.GetAsync();
        Students = studentsEntities.Select(s => s.Name).ToArray();
    }
    
    [RelayCommand]
    private async Task SaveAsync()
    {
        
        if (SelectedStudent is null)
        {
            await alertService.DisplayAsync("Student is not selected", "You must select a student when adding evaulation.");
            return;
        }
        
        if (SelectedActivity is null)
        {
            await alertService.DisplayAsync("Activity is not selected", "You must select a activity when adding evaulation.");
            return;
        }
        var student = await studentFacade.GetStudentByNameAsync(SelectedStudent);
        Evaluation.StudentId = student.Id;
        Evaluation.Student = student;
    
        var activity = await activityFacade.GetActivityByName(SelectedActivity);
        Evaluation.ActivityId = activity.Id;
        Evaluation.Activity = activity;
        await evaluationFacade.SaveAsync(Evaluation);

        MessengerService.Send(new EditMessage { Id = Evaluation.Id });

        navigationService.SendBackButtonPressed();
    }
    
}
