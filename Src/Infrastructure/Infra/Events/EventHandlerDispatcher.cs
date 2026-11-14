using Application.Interfaces.Events;
using Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Events
{
    public sealed class EventHandlerDispatcher : IDomainEventsDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public EventHandlerDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchEvents(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct)
        {
            foreach (var domainEvent in domainEvents)
            {
                // get the actual event in runtime and construct
                // ex IDomainEventHandler<ProductCreatedDomainEvent>()
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());


                // get all handlers register for this event in container
                foreach (var handler in _serviceProvider.GetServices(handlerType))
                {
                    var method = handlerType.GetMethod(nameof(IDomainEventHandler<>.Handle));

                    // call handle and pass the event
                    await (Task)method!.Invoke(
                        handler, 
                        new object[] {domainEvent, ct})!;
                }
            }
        }
    }
}
