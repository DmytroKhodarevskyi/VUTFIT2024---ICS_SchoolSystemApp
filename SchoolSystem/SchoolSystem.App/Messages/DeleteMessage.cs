using SchoolSystem.BL.Models;

namespace SchoolSystem.App.Messages;

public record DeleteMessage<T> : Message<T>
    where T : IModel
{
}