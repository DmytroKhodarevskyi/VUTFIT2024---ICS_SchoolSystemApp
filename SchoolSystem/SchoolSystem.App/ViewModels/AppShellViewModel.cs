using SchoolSystem.App.Services.Interfaces;

namespace SchoolSystem.App.ViewModels;

public class AppShellViewModel : ViewModelBase
{
    public Guid SubjectId { get; set; }

    public AppShellViewModel(IMessengerService messengerService) : base(messengerService)
    {
    }
}