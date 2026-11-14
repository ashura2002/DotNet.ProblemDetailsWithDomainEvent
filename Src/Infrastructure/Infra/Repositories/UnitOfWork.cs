
using Application.Interfaces.Events;
using Application.Interfaces.Uof;
using Domain.Entities;
using Domain.Events;
using Infra.Data.DbContexts;

namespace Infra.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DemoDbContext _context;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        public UnitOfWork(DemoDbContext context, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _context = context;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            // every SaveChanges enters the loop and stops when there are no domain events
            while (true)
            {
                var domainEvents = CollectAndClearEvents();
                // stop processing when there are no domain events left
                if (domainEvents.Count == 0)
                {
                    break;
                }
                await _domainEventsDispatcher.DispatchEvents(domainEvents, ct);
            }

            await _context.SaveChangesAsync(ct);
        }


        private List<IDomainEvent> CollectAndClearEvents()
        {
            var aggregates = _context.ChangeTracker
                .Entries<Aggregate>()
                .Select(entry => entry.Entity)
                .Where(entry => entry.DomainEvents.Count > 0)
                .ToList();

            var domainEvents = aggregates
                .SelectMany(aggregate => aggregate.DomainEvents)
                .ToList();

            foreach (var item in aggregates)
            {
                item.ClearEvents();
            }

            return domainEvents;
        }
    }
}
