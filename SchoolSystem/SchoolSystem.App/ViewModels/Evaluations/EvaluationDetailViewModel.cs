using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;
using System;
using System.Threading.Tasks;

namespace SchoolSystem.App.ViewModels.Evaluations;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class EvaluationDetailViewModel : ViewModelBase, IRecipient<EditMessage>
{
    private readonly IEvaluationFacade evaluationFacade;
    private readonly IStudentFacade studentFacade;
    private readonly IActivityFacade activityFacade;
    private readonly INavigationService navigationService;
    private readonly IAlertService alertService;

    public Guid Id { get; set; }
    public EvaluationDetailModel? Evaluation { get; private set; }
    public ActivityDetailModel? Activity { get; private set; }

    public EvaluationDetailViewModel(
        IEvaluationFacade evaluationFacade,
        IStudentFacade studentFacade,
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IMessengerService messengerService,
        IAlertService alertService)
        : base(messengerService)
    {
        this.evaluationFacade = evaluationFacade;
        this.studentFacade = studentFacade;
        this.activityFacade = activityFacade;
        this.navigationService = navigationService;
        this.alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Evaluation = await evaluationFacade.GetAsync(Id);
        if (Evaluation != null)
        {
            Evaluation.StudentName = await studentFacade.GetStudentNameByIdAsync(Evaluation.StudentId);
            Evaluation.StudentSurname = await studentFacade.GetStudentSurnameByIdAsync(Evaluation.StudentId);
            Activity = await activityFacade.GetAsync(Evaluation.ActivityId);
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Evaluation is not null)
        {
            try
            {
                await evaluationFacade.DeleteAsync(Evaluation.Id);
                MessengerService.Send(new DeleteMessage<EvaluationListModel>());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Error", "Evaluation cannot be deleted.");
            }
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(EvaluationEditViewModel.Evaluation)] = Evaluation });
    }

    [RelayCommand]
    private async Task SaveDescriptionAsync()
    {
        if (Evaluation is not null)
        {
            try
            {
                await evaluationFacade.SaveAsync(Evaluation);
            }
            catch (Exception ex)
            {
                await alertService.DisplayAsync("Error", $"Failed to save evaluation description: {ex.Message}");
            }
        }
        MessengerService.Send(new EditMessage { Id = Evaluation.Id });

        navigationService.SendBackButtonPressed();
    }
    public async void Receive(EditMessage message)
    {
        if (message.Id == Evaluation?.Id)
        {
            await LoadDataAsync();
        }
    }
}
