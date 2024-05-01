using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.App.ViewModels;
using SchoolSystem.App.ViewModels.Evaluations;
using SchoolSystem.App.ViewModels.Students;
using SchoolSystem.App.ViewModels.Subjects;
using SchoolSystem.App.ViewModels.Activity;


namespace SchoolSystem.App.Shells;

public partial class AppShell : Shell
{
    private readonly INavigationService _navigationService;
    private AppShellViewModel _appShellViewModel;

    public AppShell(INavigationService navigationService, IMessengerService messengerService)
    {
        InitializeComponent();
        _appShellViewModel = new AppShellViewModel(messengerService);
        _navigationService = navigationService;
        BindingContext = _appShellViewModel;
    }
    
    [RelayCommand]
    private async Task GoToSubjectAsync()
        => await _navigationService.GoToAsync<SubjectListViewModel>();

}