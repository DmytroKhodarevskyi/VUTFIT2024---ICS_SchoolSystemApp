using SchoolSystem.BL.Models;

namespace SchoolSystem.App.Messages;

public record AddedMessage<T> : Message<T>
    where T : IModel
{
}