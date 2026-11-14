
using Domain.Events;

namespace Domain.Entities
{
    public abstract class Aggregate
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        
       protected void RaiseEvent(IDomainEvent domainEvent)
        {
            Console.WriteLine("Event Raise.");
            _domainEvents.Add(domainEvent);
        }

        public void ClearEvents()
        {
            Console.WriteLine("Events Cleared.");
            _domainEvents.Clear();
        }

    }
}