using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolSystem.App.ViewModels.Activity;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityDetailViewModel(
    IActivityFacade activityFacade,
    ISubjectFacade subjectFacade,
    IStudentFacade studentFacade,
    IEvaluationFacade evaluationFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<EditMessage>
{
    public Guid Id { get; set; }
    public ActivityDetailModel? Activity { get; private set; }
    public SubjectDetailedModel? Subject { get; private set; }
    public ObservableCollection<StudentListModel> Students { get; private set; } = new();
    public ObservableCollection<EvaluationListModel> Evaluations { get; private set; } = new();

    // Temporary storage for student points
    private readonly Dictionary<Guid, string> _studentPoints = new();

    public string GetStudentPoints(Guid studentId)
    {
        _studentPoints.TryGetValue(studentId, out var points);
        return points ?? string.Empty;
    }

    public void SetStudentPoints(Guid studentId, string points)
    {
        if (_studentPoints.ContainsKey(studentId))
        {
            _studentPoints[studentId] = points;
        }
        else
        {
            _studentPoints.Add(studentId, points);
        }
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activity = await activityFacade.GetAsync(Id);
        Subject = await subjectFacade.GetAsync(Activity!.SubjectId);
        var evaluations = await evaluationFacade.GetAsyncListByActivity(Id);
        var students = await studentFacade.GetStudentsBySubjectAsync(Subject!.Abbreviation);

        Students.Clear();
        foreach (var student in students)
        {
            var evaluation = evaluations.FirstOrDefault(e => e.StudentId == student.Id);
            SetStudentPoints(student.Id, evaluation?.Score.ToString() ?? string.Empty);
            Students.Add(student);
        }

        Evaluations.Clear();
        foreach (var evaluation in evaluations)
        {
            Evaluations.Add(evaluation);
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Activity is not null)
        {
            try
            {
                await activityFacade.DeleteAsync(Activity.Id);
                MessengerService.Send(new DeleteMessage<ActivityListModel>());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Error", "Activity cannot be deleted.");
            }
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(ActivityEditViewModel.Activity)] = Activity });
    }

    [RelayCommand]
    private async Task AddOrUpdateEvaluationAsync(Guid studentId)
    {
        if (_studentPoints.TryGetValue(studentId, out var pointsText) && int.TryParse(pointsText, out int points))
        {
            var evaluation = await evaluationFacade.GetEvaluationByStudentAndActivity(studentId, Activity!.Id);

            if (evaluation == null)
            {
                evaluation = new EvaluationDetailModel
                {
                    ActivityId = Activity!.Id,
                    StudentId = studentId,
                    Score = points
                };
                await evaluationFacade.SaveAsync(evaluation);
                Evaluations.Add(new EvaluationListModel { Id = evaluation.Id, StudentId = studentId, Score = points });
            }
            else
            {
                evaluation.Score = points;
                await evaluationFacade.SaveAsync(evaluation);
            }
            await LoadDataAsync();
        }
        else
        {
            await alertService.DisplayAsync("Error", "Invalid points input.");
        }
    }

    public async void Receive(EditMessage message)
    {
        if (message.Id == Activity?.Id)
        {
            await LoadDataAsync();
        }
    }
}
