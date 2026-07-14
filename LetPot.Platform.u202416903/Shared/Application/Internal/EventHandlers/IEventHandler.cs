using Cortex.Mediator.Notifications;
using LetPot.Platform.u202416903.Shared.Domain.Model.Events;

namespace LetPot.Platform.u202416903.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}
