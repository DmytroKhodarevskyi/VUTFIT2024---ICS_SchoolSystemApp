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

//public partial class ActivityListViewModel(
//    IActivityFacade activityFacade,
//    ISubjectFacade subjectFacade,
//    INavigationService navigationService,
//    IMessengerService messengerService)
//    : ViewModelBase(messengerService), IRecipient<EditMessage>, IRecipient<DeleteMessage<ActivityListModel>>

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
    //public string SelectedSubject { get; set; } = string.Empty;

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

    //public string SelectedSubject
    //{
    //    get => _selectedSubject;
    //    set
    //    {
    //        if (_selectedSubject != value)
    //        {
    //            _selectedSubject = value;
    //            OnPropertyChanged(nameof(SelectedSubject));
    //
    //            if (value != null) // Only reload data if a valid selection is made
    //            {
    //                LoadDataAsync();
    //            }
    //        }
    //    }
    //}



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
                OnPropertyChanged(nameof(SelectedFilter));
                LoadDataAsync();
            }
        }
    }

    private string _selectedFilter = "NoFilter";
    // public string SelectedFilter
    // {
    //     get => _selectedFilter;
    //     set
    //     {
    //         if (_selectedFilter != value)
    //         {
    //             _selectedFilter = value;
    //             OnPropertyChanged(nameof(SelectedFilter));
    //             ParseInterval(_selectedFilter);
    //             LoadDataAsync();  // Ensure this is called to refresh the list
    //         }
    //     }
    // }
    
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


    //private string _selectedFilterTag;
    //public string SelectedFilterTag
    //{
    //    get => _selectedFilterTag;
    //    set
    //    {
    //        if (_selectedFilterTag != value)
    //        {
    //            _selectedFilterTag = value;
    //            OnPropertyChanged(nameof(SelectedFilterTag));
    //            ParseInterval(_selectedFilterTag);
    //            LoadDataAsync();  // Ensure this is called to refresh the list
    //        }
    //    }
    //}

    public Interval Interval { get; set; } = Interval.NoFilter;

    //public DateTime? FilterStart { get; set; } = null;
    private DateTime? _filterStart = DateTime.Now.AddYears(-5).AddHours(12);

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

    private DateTime? _filterEnd = DateTime.Now.AddHours(12);
    //public DateTime? FilterEnd { get; set; } = null;

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

            LoadDataAsync(); // Ensure this is called to refresh the list
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

    //protected override async Task LoadDataAsync()
    //{
    //    await base.LoadDataAsync();
    //
    //    // Fetch Activities
    //    Activities = await activityFacade.GetAsync();
    //
    //    // Fetch Subjects and maintain the current selection if it still exists
    //    var previousSubject = _selectedSubject;
    //    var subjectsEntities = await subjectFacade.GetAsync();
    //    Subjects = subjectsEntities.Select(s => s.Abbreviation).ToArray();
    //
    //    // Check if the previous selected subject still exists, otherwise reset
    //    if (!Subjects.Contains(previousSubject) && Subjects.Any())
    //    {
    //        _selectedSubject = Subjects.FirstOrDefault();
    //        OnPropertyChanged(nameof(SelectedSubject)); // This triggers UI update
    //    }
    //
    //    // This check is necessary to prevent unnecessary loading when the subjects are initially populated.
    //    if (_selectedSubject != previousSubject)
    //    {
    //        // Call methods that are dependent on the selected subject
    //        if (FilterStart.HasValue)
    //        {
    //            _filterStart = FilterStart.Value.Date + TimeFilterStart;
    //        }
    //        if (FilterEnd.HasValue)
    //        {
    //            _filterEnd = FilterEnd.Value.Date + TimeFilterEnd;
    //        }
    //
    //        ParseInterval(SelectedFilter);
    //
    //        if (FilterEnd == null)
    //        {
    //            FilterEnd = GetMaxTime(Activities, FilterEnd);
    //        }
    //        if (FilterStart == null)
    //        {
    //            FilterStart = GetMinTime(Activities, FilterStart);
    //        }
    //
    //        Activities = await activityFacade.GetAsyncFilter(_filterStart, _filterEnd, Tag, SelectedSort, Descending, DoFilter, _selectedSubject);
    //    }
    //}


    protected override async Task LoadDataAsync()
    {
    
        await base.LoadDataAsync();
        Activities = await activityFacade.GetAsync();
    
        var subjectsEntities = await subjectFacade.GetAsync();
        if (Subjects == null || Subjects.Length == 0)
            Subjects = subjectsEntities.Select(s => s.Abbreviation).ToArray();
        //SelectedSubject = Subjects.FirstOrDefault();
        //_selectedSubject = Subjects.FirstOrDefault();
        //if (string.IsNullOrEmpty(_selectedSubject))
        //{
        //    _selectedSubject = Subjects.FirstOrDefault();
        //}
    
        if (FilterStart.HasValue)
        {
            // Combine the date part of FilterStart with the time part of TimeFilterStart
            // FilterStart = FilterStart.Value.Date + TimeFilterStart;
            _filterStart = FilterStart.Value.Date + TimeFilterStart;
        }
        if (FilterEnd.HasValue)
        {
            // Combine the date part of FilterStart with the time part of TimeFilterStart
            // FilterEnd = FilterEnd.Value.Date + TimeFilterEnd;
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
    
        // Activities = await activityFacade.GetAsyncFilter(FilterStart, FilterEnd, Tag);
        //if (SelectedSubject != null)
        Activities = await activityFacade.GetAsyncFilter(_filterStart, _filterEnd, Tag, SelectedSort, Descending, DoFilter, SelectedSubject);
    }

    //protected override async Task LoadDataAsync()
    //{
    //    await base.LoadDataAsync();
    //
    //    // Fetching activities might not need to reset the subjects.
    //    Activities = await activityFacade.GetAsync();
    //
    //    // Fetch and update subjects only if they are likely to have changed.
    //    var subjectsEntities = await subjectFacade.GetAsync();
    //    string[] newSubjects = subjectsEntities.Select(s => s.Abbreviation).ToArray();
    //
    //    // Check if the current selected subject still exists in the new list.
    //    if (!newSubjects.Contains(_selectedSubject))
    //    {
    //        _selectedSubject = newSubjects.FirstOrDefault(); // Set to first or default to null if empty.
    //        OnPropertyChanged(nameof(SelectedSubject)); // Notify any bound controls to update.
    //    }
    //
    //    Subjects = newSubjects; // Update the subjects list last to avoid race conditions with the selected subject.
    //
    //    // Consider if you need to reload activities based on the selected subject or other filters.
    //    if (FilterStart.HasValue)
    //    {
    //        _filterStart = FilterStart.Value.Date + TimeFilterStart;
    //    }
    //    if (FilterEnd.HasValue)
    //    {
    //        _filterEnd = FilterEnd.Value.Date + TimeFilterEnd;
    //    }
    //
    //    ParseInterval(SelectedFilter);
    //
    //    if (FilterEnd == null)
    //    {
    //        FilterEnd = GetMaxTime(Activities, FilterEnd);
    //    }
    //    if (FilterStart == null)
    //    {
    //        FilterStart = GetMinTime(Activities, FilterStart);
    //    }
    //
    //    // Use the updated filter parameters including the potentially updated selected subject.
    //    Activities = await activityFacade.GetAsyncFilter(_filterStart, _filterEnd, Tag, SelectedSort, Descending, DoFilter, _selectedSubject);
    //}

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
    // private void ParseInterval(string selectedFilter)
    // {
    //     if (selectedFilter == "NoFilter")
    //         ManualFilter = true;
    //     else
    //         ManualFilter = false;
    //
    //     if (ManualFilter)
    //         return;
    //
    //     if (selectedFilter == null)
    //         return;
    //
    //     Interval = (Interval)Enum.Parse(typeof(Interval), SelectedFilter);
    //
    //     DateTime now = DateTime.Now;
    //     switch (Interval)
    //     {
    //         case Interval.Last24Hours:
    //             //FilterStart = now.AddDays(-1);
    //             _filterStart = now.AddDays(-1);
    //             //FilterEnd = now;  // Assuming you want to include up to the current moment.
    //             _filterEnd = now;
    //             break;
    //         case Interval.Last7Days:
    //             //FilterStart = now.AddDays(-7);
    //             _filterStart = now.AddDays(-7);
    //             //FilterEnd = now;
    //             _filterEnd = now;
    //             break;
    //         case Interval.CurrentMonth:
    //             // FilterStart = new DateTime(now.Year, now.Month, 1);
    //             // FilterEnd = FilterStart.Value.AddMonths(1).AddDays(-1);
    //             _filterStart = new DateTime(now.Year, now.Month, 1);
    //             _filterEnd = _filterStart.Value.AddMonths(1).AddDays(-1);
    //             break;
    //         case Interval.PreviousMonth:
    //             // FilterStart = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
    //             // FilterEnd = new DateTime(now.Year, now.Month, 1).AddDays(-1);
    //             _filterStart = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
    //             _filterEnd = new DateTime(now.Year, now.Month, 1).AddDays(-1);
    //             break;
    //         case Interval.LastYear:
    //             // FilterStart = now.AddYears(-1);
    //             // FilterEnd = now;
    //             _filterStart = now.AddYears(-1);
    //             _filterEnd = now;
    //             break;
    //         case Interval.NoFilter:
    //             // Assuming no need to set FilterStart or FilterEnd as we want all data
    //             // FilterStart = null;
    //             // FilterEnd = null;
    //             _filterStart = null;
    //             _filterEnd = null;
    //             break;
    //         default:
    //             throw new Exception("Undefined interval");
    //     }
    //
    //     //FilterEnd = now;
    //     _filterEnd = now;
    // }

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
        //LoadDataAsync();
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