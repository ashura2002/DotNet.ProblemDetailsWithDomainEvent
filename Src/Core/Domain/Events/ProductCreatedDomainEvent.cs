namespace Domain.Events
{
    public sealed record ProductCreatedDomainEvent(Guid ProductId) : IDomainEvent;
}
