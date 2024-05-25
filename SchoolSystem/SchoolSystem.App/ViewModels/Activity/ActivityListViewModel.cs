using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static SchoolSystem.BL.Facades.Interfaces.IActivityFacade;

namespace SchoolSystem.App.ViewModels.Activity;


public partial class ActivityListViewModel : ViewModelBase, IRecipient<EditMessage>, IRecipient<DeleteMessage<ActivityListModel>>
{
    private readonly IActivityFacade activityFacade;
    private readonly ISubjectFacade subjectFacade;
    private readonly INavigationService navigationService;

    public ActivityListViewModel(
        IActivityFacade activityFacade,
        ISubjectFacade subjectFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        this.activityFacade = activityFacade;
        this.subjectFacade = subjectFacade;
        this.navigationService = navigationService;
        LoadDataAsync(); // Initialize and load data
    }


    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    public string[] Subjects { get; private set; } = Array.Empty<string>();

    private string _selectedSubject = string.Empty;

    public string SelectedSubject
    {
        get => _selectedSubject;
        set
        {
            if (_selectedSubject != value)
            {
                if (value != null)
                {
                    _selectedSubject = value;
                    OnPropertyChanged(nameof(SelectedSubject));
                    LoadDataAsync();
                }
               
            }
        }
    }




    private int _tag = 0;
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

    public string[] Sort { get; set; } = Enum.GetNames(typeof(SortBy));

    private string _selectedSort= "Subject";

    public string SelectedSort
    {
        get => _selectedSort;
        set
        {
            if (_selectedSort != value)
            {
                _selectedSort = value;
                OnPropertyChanged(nameof(SelectedSort));
                LoadDataAsync();
            }
        }
    }

    private string _selectedFilter = "NoFilter";

    
    public string SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            if (_selectedFilter != value)
            {
                _selectedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));
                ParseInterval(_selectedFilter);  // Ensures that interval changes apply immediately
                if (ManualFilter)
                {
                    LoadDataAsync();
                }
            }
        }
    }


    public Interval Interval { get; set; } = Interval.NoFilter;

    private DateTime? _filterStart = DateTime.Now.AddHours(12);

    public DateTime? FilterStart
    {
        get => _filterStart;
        set
        {
            if (_filterStart != value)
            {
                _filterStart = value;
                if (!ManualFilter)
                {
                    OnPropertyChanged(nameof(FilterStart));
                    ParseInterval(_selectedFilter);

                }
                LoadDataAsync(); // Ensure this is called to refresh the list
            }
        }
    }

    private DateTime? _filterEnd = DateTime.Now.AddDays(2).AddHours(12);

   public DateTime? FilterEnd
   {
       get => _filterEnd;
       set
       {
           if (_filterEnd != value)
           {
               _filterEnd = value;
               if (!ManualFilter)
               {
                   OnPropertyChanged(nameof(FilterEnd));
                   ParseInterval(_selectedFilter);
               }

                    
               LoadDataAsync(); // Ensure this is called to refresh the list
           }
       }
   }

   private TimeSpan _timefilterstart;
   public TimeSpan TimeFilterStart
   {
       get => _timefilterstart;
       set
       {
           if (_timefilterstart != value)
           {
               _timefilterstart = value;
               if (!ManualFilter)
               {
                   OnPropertyChanged(nameof(TimeFilterStart));
                   ParseInterval(_selectedFilter);
               }
               LoadDataAsync(); // Ensure this is called to refresh the list
           }
       }
    }

   private TimeSpan _timefilterend;
   public TimeSpan TimeFilterEnd
   {
       get => _timefilterend;
       set
       {
           if (_timefilterend != value)
           {
               _timefilterend = value;
               if (!ManualFilter)
               {
                   OnPropertyChanged(nameof(TimeFilterEnd));
                   ParseInterval(_selectedFilter);
               }
               LoadDataAsync(); // Ensure this is called to refresh the list
           }
       }
    }

    public bool ManualFilter = true;

    private bool _descending = false;

    public bool Descending
    {
        get => _descending;
        set
        {
            _descending = value;
            OnPropertyChanged(nameof(Descending));

            LoadDataAsync(); 
        }
    }


    private bool _dofilter = false;

    public bool DoFilter
    {
        get => _dofilter;
        set
        {
            _dofilter= value;
            OnPropertyChanged(nameof(DoFilter));

            LoadDataAsync(); // Ensure this is called to refresh the list
        }
    }

   


    protected override async Task LoadDataAsync()
    {
    
        await base.LoadDataAsync();
        Activities = await activityFacade.GetAsync();
    
        var subjectsEntities = await subjectFacade.GetAsync();
        if (Subjects == null || Subjects.Length == 0 || _selectedSubject == String.Empty)
            Subjects = subjectsEntities.Select(s => s.Abbreviation).ToArray();
      
    
        if (FilterStart.HasValue)
        {
            // Combine the date part of FilterStart with the time part of TimeFilterStart
            _filterStart = FilterStart.Value.Date + TimeFilterStart;
        }
        if (FilterEnd.HasValue)
        {
            // Combine the date part of FilterStart with the time part of TimeFilterStart
            _filterEnd = FilterEnd.Value.Date + TimeFilterEnd;
        }
    
        ParseInterval(SelectedFilter);
    
        if (FilterEnd == null)
        {
            FilterEnd = GetMaxTime(Activities, FilterEnd);
        }
        if (FilterStart == null)
        {
            FilterStart = GetMinTime(Activities, FilterStart);
        }
    
        Activities = await activityFacade.GetAsyncFilter(_filterStart, _filterEnd, Tag, SelectedSort, Descending, DoFilter, SelectedSubject);
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
        if (selectedFilter == null || !ManualFilter) return;

        Interval = (Interval)Enum.Parse(typeof(Interval), selectedFilter);
        DateTime now = DateTime.Now;

        switch (Interval)
        {
            case Interval.Last24Hours:
                _filterStart = now.AddDays(-1);
                _filterEnd = now;
                break;
            case Interval.Last7Days:
                _filterStart = now.AddDays(-7);
                _filterEnd = now;
                break;
            case Interval.CurrentMonth:
                _filterStart = new DateTime(now.Year, now.Month, 1);
                _filterEnd = _filterStart.Value.AddMonths(1).AddDays(-1);
                break;
            case Interval.PreviousMonth:
                _filterStart = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                _filterEnd = new DateTime(now.Year, now.Month, 1).AddDays(-1);
                break;
            case Interval.LastYear:
                _filterStart = now.AddYears(-1);
                _filterEnd = now;
                break;
            case Interval.NoFilter:
                break;
            default:
                throw new Exception("Undefined interval");
        }

        // Only trigger updates if in manual filter mode
        OnPropertyChanged(nameof(FilterStart));
        OnPropertyChanged(nameof(FilterEnd));
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

    [RelayCommand]
    public void ResetTag()
    {
        Tag = 0;
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