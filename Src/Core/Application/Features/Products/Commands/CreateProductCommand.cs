using Application.Common.Results;
using MediatR;

namespace Application.Features.Products.Commands
{
    public sealed record CreateProductCommand(
        string Name,
        string Description) : IRequest<ResultWithReturnValue<Guid>>;
}
