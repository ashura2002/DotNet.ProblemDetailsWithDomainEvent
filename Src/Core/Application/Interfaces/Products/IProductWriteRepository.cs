using Domain.Entities;

namespace Application.Interfaces.Products
{
    public interface IProductWriteRepository
    {
        void Add(Product product);
        void Remove(Product product);
        Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken ct);
    }
}
