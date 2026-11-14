using Application.Common.Results;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Queries
{
    public sealed record GetAllProductsQuery(int Page, int PageSize) : IRequest<ResultWithReturnValue<PaginationResponse<ProductResponse>>>;
}
