using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.App.ViewModels.Students;
<<<<<<< HEAD
using SchoolSystem.App.ViewModels.Subjects;
=======
using SchoolSystem.App.ViewModels.Activity;

>>>>>>> viewmodelactivity

namespace SchoolSystem.App.Shells;

public partial class AppShell
{
    private readonly INavigationService _navigationService;

    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
    }

    // Example method to navigate to the student list view
    [RelayCommand]
    private async Task GoToStudentAsync()
        => await _navigationService.GoToAsync<StudentListViewModel>();

<<<<<<< HEAD

    [RelayCommand]
    private async Task GoToSubjectAsync()
        => await _navigationService.GoToAsync<SubjectListViewModel>();
=======
    // Example method to navigate to the student list view
    [RelayCommand]
    private async Task GoToActivitiesAsync()
        => await _navigationService.GoToAsync<ActivityListViewModel>();
>>>>>>> viewmodelactivity
}