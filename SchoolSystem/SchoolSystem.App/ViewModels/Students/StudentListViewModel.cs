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
                var filtered = Students
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
