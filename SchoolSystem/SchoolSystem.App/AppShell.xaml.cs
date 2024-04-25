using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.App.ViewModels.Students;

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
