using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services.Interfaces;
using SchoolSystem.BL.Facades.Interfaces;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Students;

public partial class StudentListViewModel(
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<EditMessage>, IRecipient<DeleteMessage<StudentListModel>>
{

    public IEnumerable<StudentListModel> Students { get; set; } = null!;    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Students = await studentFacade.GetAsync();
        
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
    
}