using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Services.Interfaces;

namespace SchoolSystem.App.Services;

public class MessengerService(IMessenger messenger) : IMessengerService
{
    public IMessenger Messenger { get; } = messenger;

    public void Send<TMessage>(TMessage message)
        where TMessage : class
    {
        Messenger.Send(message);
    }
}