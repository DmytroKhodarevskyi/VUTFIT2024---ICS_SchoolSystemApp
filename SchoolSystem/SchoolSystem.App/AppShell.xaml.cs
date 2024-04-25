using SchoolSystem.App.Services.Interfaces;

namespace SchoolSystem.App
{
    public partial class AppShell
    {
        private readonly INavigationService _navigationService;

        public AppShell(INavigationService navigationService)
        {
            _navigationService = navigationService;

            InitializeComponent();
        }

        [RelayCommand]
        private async Task GoToStudentsAsync()
            => await _navigationService.GoToAsync<StudentListViewModel>();

       
    }
}
