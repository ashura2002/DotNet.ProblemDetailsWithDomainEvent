namespace Application.Responses
{
    public sealed record ProductResponse(
        Guid Id,
        string Name,
        string? Description,
        DateTime CreatedAt);
}
