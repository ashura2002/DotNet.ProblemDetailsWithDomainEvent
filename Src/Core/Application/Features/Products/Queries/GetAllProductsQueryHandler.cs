using Application.Common.Results;
using Application.Interfaces.Products;
using Application.Responses;
using MediatR;

namespace Application.Features.Products.Queries
{
    public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ResultWithReturnValue<PaginationResponse<ProductResponse>>>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<ResultWithReturnValue<PaginationResponse<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productReadRepository.GetAllProductsAsync(request.Page, request.PageSize, cancellationToken);

            return ResultWithReturnValue<PaginationResponse<ProductResponse>>.Success(products);
        }
    }
}
