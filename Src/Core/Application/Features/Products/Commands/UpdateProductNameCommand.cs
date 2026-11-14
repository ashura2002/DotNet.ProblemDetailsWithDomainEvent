using Application.Common.Results;
using MediatR;

namespace Application.Features.Products.Commands
{
    public sealed record UpdateProductNameCommand(Guid ProductId, string NewProductName) : IRequest<Result>;
}
