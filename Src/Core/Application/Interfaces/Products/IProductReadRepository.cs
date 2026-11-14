using Application.Responses;

namespace Application.Interfaces.Products
{
    public interface IProductReadRepository
    {
        Task<ProductResponse?> GetProductByIdAsync(Guid productId, CancellationToken ct);
        Task<bool> IsProductNameExistAsync(string productName, CancellationToken ct);
        Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(int page, int pageSize, CancellationToken ct);
    }
}
