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

    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    public Guid UserId { get; set; }

    private int _tag = -1;
    public int Tag
    {
        get => _tag;
        set
        {
            if (_tag != value)
            {
                _tag = value;
                OnPropertyChanged(nameof(Tag));
                LoadDataAsync();  // Trigger data reload when the tag changes
            }
        }
    }

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


    private string _selectedFilterTag;
    public string SelectedFilterTag
    {
        get => _selectedFilterTag;
        set
        {
            if (_selectedFilterTag != value)
            {
                _selectedFilterTag = value;
                OnPropertyChanged(nameof(SelectedFilterTag));
                ParseInterval(_selectedFilterTag);
                LoadDataAsync();  // Ensure this is called to refresh the list
            }
        }
    }

    public Interval Interval { get; set; } = Interval.All;

    public DateTime? FilterStart { get; set; } = null;

    public DateTime? FilterEnd { get; set; } = null;

    public bool ManualFilter { get; set; }


    protected override async Task LoadDataAsync()
    {

        await base.LoadDataAsync();
        Activities = await activityFacade.GetAsync();
        ParseInterval(SelectedFilter);

        if (FilterEnd == null)
        {
            FilterEnd = GetMaxTime(Activities, FilterEnd);
        }
        if (FilterStart == null)
        {
            FilterStart = GetMinTime(Activities, FilterStart);
        }
        Activities = await activityFacade.GetAsyncFilter(FilterStart, FilterEnd, Tag);
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
                FilterStart = new DateTime(now.Year, now.Month, 1);
                if (FilterStart.HasValue)
                {
                    FilterEnd = FilterStart.Value.AddMonths(1).AddDays(-1);
                }
                break;
            case Interval.Last_Month:
                FilterStart = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                FilterEnd = new DateTime(now.Year, now.Month, 1).AddDays(-1);
                break;
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



    [RelayCommand]
    private async Task GoToRefreshAsync()
    {
        await LoadDataAsync();
    }
}