using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SchoolSystem.App.ViewModels.Subjects
{
    public partial class SubjectListViewModel : ViewModelBase, IRecipient<EditMessage>, IRecipient<DeleteMessage<SubjectListModel>>
    {
        public ObservableCollection<SubjectListModel> FilteredSubjects { get; } = new ObservableCollection<SubjectListModel>();

        public string[] Sort { get; set; } = Enum.GetNames(typeof(ISubjectFacade.SortBy));

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

        private string _selectedSort = "Name";

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

        [ObservableProperty]
        private string searchQuery = string.Empty;

        public IEnumerable<SubjectListModel> Subjects { get; set; } = null!;

        public SubjectListViewModel(ISubjectFacade subjectFacade, INavigationService navigationService, IMessengerService messengerService)
            : base(messengerService)
        {
            this.subjectFacade = subjectFacade;
            this.navigationService = navigationService;
            this.messengerService = messengerService;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();
            Subjects = await subjectFacade.GetAsync();
            FilterSubjects(); 
        }

        partial void OnSearchQueryChanged(string value)
        {
            FilterSubjects();
        }

        private void FilterSubjects()
        {
            if (Subjects != null)
            {
                IEnumerable<SubjectListModel> sorted;

                // Sorting based on the SelectedSort property
                if (SelectedSort == "Name")
                {
                    sorted = Descending 
                        ? Subjects.OrderByDescending(e => e.Name) 
                        : Subjects.OrderBy(e => e.Name);
                }
                else if (SelectedSort == "Abbreviation")
                {
                    sorted = Descending
                        ? Subjects.OrderByDescending(e => e.Abbreviation)
                        : Subjects.OrderBy(e => e.Abbreviation);
                }
                else
                {
                    sorted = Subjects; // Default no sort
                }

                var filtered = sorted
                    .Where(s => s.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                                s.Abbreviation.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredSubjects.Clear();
                foreach (var subject in filtered)
                {
                    FilteredSubjects.Add(subject);
                }
            }
        }

        [RelayCommand]
        private async Task GoToCreateAsync()
        {
            await navigationService.GoToAsync("/edit");
        }

        [RelayCommand]
        private async Task GoToDetailAsync(Guid id)
        {
            await navigationService.GoToAsync<SubjectDetailViewModel>(
                new Dictionary<string, object?> { [nameof(SubjectDetailViewModel.Id)] = id });
        }

        public async void Receive(EditMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(DeleteMessage<SubjectListModel> message)
        {
            await LoadDataAsync();
        }

        private readonly ISubjectFacade subjectFacade;
        private readonly INavigationService navigationService;
        private readonly IMessengerService messengerService;
    }
}
