
using Domain.Events;

namespace Application.Interfaces.Events
{
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent, CancellationToken ct);
    }
}
