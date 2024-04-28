using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Evaluations;

public partial class EvaluationListViewModel(
    IEvaluationFacade evaluationFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<EditMessage>, IRecipient<DeleteMessage<EvaluationListModel>>
{
    public IEnumerable<EvaluationListModel> Evaluations { get; set; } = evaluationFacade.GetAsync().Result;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Evaluations = await evaluationFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<EvaluationDetailViewModel>(
            new Dictionary<string, object?> { [nameof(EvaluationDetailViewModel.Id)] = id });
    }

    public async void Receive(EditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(DeleteMessage<EvaluationListModel> message)
    {
        await LoadDataAsync();
    }
}