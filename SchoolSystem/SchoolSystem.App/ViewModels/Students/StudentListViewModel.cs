using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;
using System.Collections.ObjectModel;

namespace SchoolSystem.App.ViewModels.Students
{
    public partial class StudentListViewModel : ViewModelBase, IRecipient<EditMessage>, IRecipient<DeleteMessage<StudentListModel>>
    {
        public ObservableCollection<StudentListModel> FilteredStudents { get; } = new ObservableCollection<StudentListModel>();

        [ObservableProperty]
        private string searchQuery = string.Empty;

        public string[] Sort { get; set; } = Enum.GetNames(typeof(IStudentFacade.SortBy));

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
        public IEnumerable<StudentListModel> Students { get; set; } = null!;

        public StudentListViewModel(IStudentFacade studentFacade, INavigationService navigationService, IMessengerService messengerService)
            : base(messengerService)
        {
            this.studentFacade = studentFacade;
            this.navigationService = navigationService;
            this.messengerService = messengerService;
        }



        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();
            Students = await studentFacade.GetAsync();
            FilterStudents(); 
        }

        partial void OnSearchQueryChanged(string value)
        {
            FilterStudents();
        }

        private void FilterStudents()
        {
            if (Students != null)
            {
                IEnumerable<StudentListModel> sorted;

                // Sorting based on the SelectedSort property
                if (SelectedSort == "Name")
                {
                    //sorted = Students.OrderBy(s => s.Name);
                    sorted = Descending
                        ? Students.OrderByDescending(e => e.Name)
                        : Students.OrderBy(e => e.Name);
                }
                else if (SelectedSort == "Surname")
                {
                    sorted = Descending
                        ? Students.OrderByDescending(e => e.Surname)
                        : Students.OrderBy(e => e.Surname);
                }
                else
                {
                    sorted = Students; // Default no sort
                }

                var filtered = sorted
                    .Where(s => s.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                                s.Surname.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredStudents.Clear();
                foreach (var student in filtered)
                {
                    FilteredStudents.Add(student);
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
            await navigationService.GoToAsync<StudentDetailViewModel>(
                new Dictionary<string, object?> { [nameof(StudentDetailViewModel.Id)] = id });
        }

        public async void Receive(EditMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(DeleteMessage<StudentListModel> message)
        {
            await LoadDataAsync();
        }

        private readonly IStudentFacade studentFacade;
        private readonly INavigationService navigationService;
        private readonly IMessengerService messengerService;
    }
}
