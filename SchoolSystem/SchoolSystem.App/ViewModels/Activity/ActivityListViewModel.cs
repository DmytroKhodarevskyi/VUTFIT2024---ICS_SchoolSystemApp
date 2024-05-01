using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Activity;

public partial class ActivityListViewModel: ViewModelBase, IRecipient<EditMessage>, IRecipient<DeleteMessage<ActivityListModel>>
{
    private readonly IActivityFacade activityFacade;
    private readonly INavigationService navigationService;
    public ActivityListViewModel(IActivityFacade _activityFacade,
        INavigationService _navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        activityFacade = _activityFacade;
        navigationService = _navigationService;
        var viewModel = (AppShellViewModel)Shell.Current.BindingContext;
        StudentId = viewModel.SubjectId;
    }
    public Guid StudentId { get; set; }
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activities = await activityFacade.GetAsync();
        // Activities = await activityFacade.GetAsyncListByStudent(StudentId);
        // ManualFilter = false;
        // ParseInterval(SelectedFilter);
        //
        // if (FilterEnd == null)
        // {
        //     FilterEnd = GetMaxTime(Activities, FilterEnd);
        // }
        // if (FilterStart == null)
        // {
        //     FilterStart = GetMinTime(Activities, FilterStart);
        // }
        //
        // Activities = await activityFacade.GetAsyncFilter(StudentId, FilterStart, FilterEnd, null, null);
    }

    [RelayCommand]
    private async Task GoToCreateAsync(Guid userId)
    {
        await navigationService.GoToAsync($"/edit?studentId={userId}");
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object> { [nameof(ActivityDetailViewModel.Id)] = id });
    }

    public async void Receive(EditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(DeleteMessage<ActivityListModel> message)
    {
        await LoadDataAsync();
    }
}