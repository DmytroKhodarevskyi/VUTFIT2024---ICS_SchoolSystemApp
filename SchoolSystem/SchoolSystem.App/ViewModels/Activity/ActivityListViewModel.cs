using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;
using System.ComponentModel;
using static SchoolSystem.BL.Facades.Interfaces.IActivityFacade;

namespace SchoolSystem.App.ViewModels.Activity;

public partial class ActivityListViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<EditMessage>, IRecipient<DeleteMessage<ActivityListModel>>
{
    public IEnumerable<ActivityListModel> Activities { get; set; } = activityFacade.GetAsync().Result;

    public Guid UserId { get; set; }

    public string[] Filters { get; set; } = Enum.GetNames(typeof(Interval));

    private string _selectedFilter;
    public string SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            if (_selectedFilter != value)
            {
                _selectedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));
                ParseInterval(_selectedFilter);
                LoadDataAsync();  // Ensure this is called to refresh the list
            }
        }
    }
    //public string SelectedFilter { get; set; } = Enum.GetName<Interval>(Interval.All);

    public Interval Interval { get; set; } = Interval.All;

    public DateTime? FilterStart { get; set; } = DateTime.Now;

    public DateTime? FilterEnd { get; set; } = DateTime.Now;

    public bool ManualFilter { get; set; }

    protected override async Task LoadDataAsync()
    {

       // await base.LoadDataAsync();
       // Activities = await activityFacade.GetAsync();

        await base.LoadDataAsync();
        Activities = await activityFacade.GetAsync();
        ManualFilter = false;
        ParseInterval(SelectedFilter);

        if (FilterEnd == null)
        {
            FilterEnd = GetMaxTime(Activities, FilterEnd);
        }
        if (FilterStart == null)
        {
            FilterStart = GetMinTime(Activities, FilterStart);
        }

        Activities = await activityFacade.GetAsyncFilter(FilterStart, FilterEnd); //TODO
    }

    public static DateTime? GetMinTime(IEnumerable<ActivityListModel> userActivities, DateTime? Start)
    {
        foreach (var userActivity in userActivities)
        {
            if (userActivity.Start < Start)
            {
                Start = userActivity.Start;
            }
        }
        return Start;
    }
    public static DateTime? GetMaxTime(IEnumerable<ActivityListModel> userActivities, DateTime? End)
    {
        foreach (var userActivity in userActivities)
        {
            if (userActivity.End > End)
            {
                End = userActivity.End;
            }
        }
        return End;
    }
    private void ParseInterval(string selectedFilter)
    {
        if (selectedFilter == null)
            return;

        Interval = (Interval)Enum.Parse(typeof(Interval), SelectedFilter);

        DateTime now = DateTime.Now;
        switch (Interval)
        {
            case Interval.Daily:
                FilterStart = now.AddDays(-1);
                break;
            case Interval.Weekly:
                FilterStart = now.AddDays(-7);
                break;
            case Interval.This_Month:
                FilterStart = DateTime.MinValue.AddYears(now.Year - 1).AddMonths(now.Month - 1); //first day of month
                break;
            case Interval.Last_Month:
                FilterStart = DateTime.MinValue.AddYears(now.Year - 1).AddMonths(now.Month - 2); //first day of month
                FilterEnd = DateTime.MinValue.AddYears(now.Year - 1).AddMonths(now.Month - 1).AddDays(-1); //last day of month
                return;
            case Interval.Yearly:
                FilterStart = now.AddYears(-1);
                break;
            case Interval.All:
                FilterStart = GetMinTime(Activities, FilterStart);
                FilterEnd = GetMaxTime(Activities, FilterEnd);
                return;
            default:
                throw new Exception("Undefined interval");
        }
        FilterEnd = now;
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });
    }

    public async void Receive(EditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(DeleteMessage<ActivityListModel> message)
    {
        await LoadDataAsync();
    }

    public async void DatePicker_PropertyChanged(object sender, SelectionChangedEventArgs e)
    {
        await GoToRefreshAsync();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    [RelayCommand]
    private async Task GoToRefreshAsync()
    {
        await LoadDataAsync();
    }
}