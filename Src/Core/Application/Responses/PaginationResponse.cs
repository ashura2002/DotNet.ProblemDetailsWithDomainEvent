namespace Application.Responses
{
    public sealed record PaginationResponse<T>(
        int Page, 
        int PageSize, 
        int TotalCount, 
        IReadOnlyCollection<T> Values);
}
