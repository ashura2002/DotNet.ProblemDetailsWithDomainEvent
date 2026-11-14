using Domain.Events;

namespace Application.Interfaces.Events
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEvents(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct);
    }
}