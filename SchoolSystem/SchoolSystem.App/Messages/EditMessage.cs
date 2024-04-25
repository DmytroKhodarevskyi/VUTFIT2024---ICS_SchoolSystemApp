using SchoolSystem.BL.Models;

namespace SchoolSystem.App.Messages;

public record EditMessage() : IMessage
{
    public Guid Id { get; init; }
}