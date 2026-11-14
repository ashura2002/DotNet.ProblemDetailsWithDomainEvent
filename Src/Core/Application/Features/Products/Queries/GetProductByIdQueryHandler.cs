using Application.Common.Errors;
using Application.Common.Results;
using Application.Interfaces.Products;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Queries
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ResultWithReturnValue<ProductResponse>>
    {
        private readonly IProductReadRepository _productReadRepository;
        public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<ResultWithReturnValue<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetProductByIdAsync(request.ProductId, cancellationToken);

            if (product is null)
                return ResultWithReturnValue<ProductResponse>.Failure(ProductErrors.ProductNotFound);

            return ResultWithReturnValue<ProductResponse>.Success(product);
        }
    }
}
