using SchoolSystem.App.Models;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.App.ViewModels;
using SchoolSystem.App.ViewModels.Students;
using SchoolSystem.App.Views.Student;
using SchoolSystem.App.ViewModels.Subjects;
using SchoolSystem.App.Views.Subject;
using SchoolSystem.App.ViewModels.Activity;
using SchoolSystem.App.ViewModels.Evaluations;
using SchoolSystem.App.Views.Activity;
using SchoolSystem.App.Views.Evaluations;


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
        //evaluations
        
        new("//evaluations", typeof(EvaluationListView), typeof(EvaluationListViewModel)),
        new("//evaluations/detail", typeof(EvaluationDetailView), typeof(EvaluationDetailViewModel)),
        
        new("//evaluations/edit", typeof(EvaluationEditView), typeof(EvaluationEditViewModel)),
        new("//evaluations/detail/edit", typeof(EvaluationEditView), typeof(EvaluationEditViewModel)),
       //activity
        new("//activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
        new("//activities/detail", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),
        new("//activities/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        new("//activities/detail/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
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