using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.App.ViewModels.Students;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Subjects;

public partial class SubjectListViewModel: ViewModelBase, IRecipient<EditMessage>, IRecipient<DeleteMessage<SubjectListModel>>
{
    
    public SubjectListViewModel(
        ISubjectFacade _subjectFacade,
        INavigationService _navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        subjectFacade = _subjectFacade;
        navigationService = _navigationService;
        viewModel = (AppShellViewModel)Shell.Current.BindingContext;
    }
    
    private readonly ISubjectFacade subjectFacade;
    private readonly INavigationService navigationService;
    private AppShellViewModel viewModel;

    public IEnumerable<SubjectListModel> Subjects { get; set; } = null!;

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Subjects = await subjectFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToStudentsListAsync(Guid id)
    {
        viewModel.SubjectId = id;
        await navigationService.GoToAsync<StudentListViewModel>();
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
}