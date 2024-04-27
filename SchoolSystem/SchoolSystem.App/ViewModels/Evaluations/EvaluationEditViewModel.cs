using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Evaluations;

public partial class EvaluationEditViewModel(
    IEvaluationFacade evaluationFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public EvaluationDetailModel Evaluation { get; init; } = EvaluationDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await evaluationFacade.SaveAsync(Evaluation);

        MessengerService.Send(new EditMessage { Id = Evaluation.Id });

        navigationService.SendBackButtonPressed();
    }
    
}
