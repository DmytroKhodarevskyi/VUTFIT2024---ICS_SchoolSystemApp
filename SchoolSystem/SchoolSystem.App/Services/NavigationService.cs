using SchoolSystem.App.Models;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.App.ViewModels;
using SchoolSystem.App.ViewModels.Students;
using SchoolSystem.App.Views.Student;
using SchoolSystem.App.ViewModels.Subjects;
using SchoolSystem.App.Views.Subject;

namespace SchoolSystem.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        //first parameter is the route, second is the view, third is the view model
        new("//students", typeof(StudentListView), typeof(StudentListViewModel)),
        new("//students/detail", typeof(StudentDetailView), typeof(StudentDetailViewModel)),
        
        new("//students/edit", typeof(StudentEditView), typeof(StudentEditViewModel)),
        new("//students/detail/edit", typeof(StudentEditView), typeof(StudentEditViewModel)),

        new("//subjects", typeof(SubjectListView), typeof(SubjectListViewModel)),
        new("//subjects/detail", typeof(SubjectDetailView), typeof(SubjectDetailViewModel)),

        new("//subjects/edit", typeof(SubjectEditView), typeof(SubjectEditViewModel)),
        new("//subjects/detail/edit", typeof(SubjectEditView), typeof(SubjectEditViewModel)),
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel 
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}