using Application.Interfaces.Events;
using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Events
{
    public sealed class ProductCreatedDomainEventHandler : IDomainEventHandler<ProductCreatedDomainEvent>
    {
        private readonly ILogger<ProductCreatedDomainEventHandler> _logger;

        public ProductCreatedDomainEventHandler(ILogger<ProductCreatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken ct)
        {
            _logger.LogInformation("Event Handler executing." +
                "\n New Product id {ProductID} added." +
                " \n Ikaw na bahala sa logic.", domainEvent.ProductId);

            return Task.CompletedTask;
        }

    }
}
