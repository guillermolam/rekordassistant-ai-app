using RekordAssistant.Shared.Helpers;

namespace RekordAssistant.Shared.Domain;

public interface IDomainEvent : INotification
{
	DateTime CreatedAt { get; }
}

public interface IDomainEventContext
{
	IEnumerable<IDomainEvent> GetDomainEvents();
}

public abstract class EventBase : IDomainEvent
{
	public string EventType => GetType().FullName;

	public DateTime CreatedAt { get; } = DateTimeHelper.NewDateTime();
}

public class EventWrapper(IDomainEvent @event) : INotification
{
	public IDomainEvent Event { get; } = @event;
}
