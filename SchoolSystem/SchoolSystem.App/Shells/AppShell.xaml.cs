using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.App.ViewModels.Evaluations;
using SchoolSystem.App.ViewModels.Students;
using SchoolSystem.App.ViewModels.Subjects;
using SchoolSystem.App.ViewModels.Activity;


namespace SchoolSystem.App.Shells;

public partial class AppShell
{
    private readonly INavigationService _navigationService;

    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
    }

  
    [RelayCommand]
    private async Task GoToStudentAsync()
        => await _navigationService.GoToAsync<StudentListViewModel>();


    [RelayCommand]
    private async Task GoToSubjectAsync()
        => await _navigationService.GoToAsync<SubjectListViewModel>();

    [RelayCommand]
    private async Task GoToActivitiesAsync()
        => await _navigationService.GoToAsync<ActivityListViewModel>();
    [RelayCommand]
    private async Task GoToEvaluationAsync()
        => await _navigationService.GoToAsync<EvaluationListViewModel>();

}