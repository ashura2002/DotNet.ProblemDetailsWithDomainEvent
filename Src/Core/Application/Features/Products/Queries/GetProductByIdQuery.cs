using Application.Common.Results;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Queries
{
    public sealed record GetProductByIdQuery(Guid ProductId) : IRequest<ResultWithReturnValue<ProductResponse>>;
}
