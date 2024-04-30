using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Evaluations;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class EvaluationDetailViewModel(
    IEvaluationFacade evaluationFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<EditMessage>
{
    public Guid Id { get; set; }
    public EvaluationDetailModel? Evaluation { get; private set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
    
        Evaluation = await evaluationFacade.GetAsync(Id);
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
    
    public async void Receive(EditMessage message)
    {
        if (message.Id == Evaluation?.Id)
        {
            await LoadDataAsync();
        }
    }
    
}