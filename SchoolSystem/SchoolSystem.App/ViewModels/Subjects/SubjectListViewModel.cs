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

namespace SchoolSystem.App.ViewModels.Subjects
{
    public partial class SubjectListViewModel : ViewModelBase, IRecipient<EditMessage>, IRecipient<DeleteMessage<SubjectListModel>>
    {
        public ObservableCollection<SubjectListModel> FilteredSubjects { get; } = new ObservableCollection<SubjectListModel>();

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
                var filtered = Subjects
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
