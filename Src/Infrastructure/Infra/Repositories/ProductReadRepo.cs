using Application.Interfaces.Products;
using Application.Responses;
using Infra.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public sealed class ProductReadRepo: IProductReadRepository
    {
        private readonly DemoDbContext _context;
        public ProductReadRepo(DemoDbContext context)
        {
            _context = context;            
        }

        public async Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(int page, int pageSize, CancellationToken ct)
        {
            var products = await _context.Products
                .AsNoTracking()
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProductResponse(
                    p.Id, 
                    p.Name, 
                    p.Description,
                    p.CreatedAt))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var totalCount = await _context.Products
                .AsNoTracking()
                .CountAsync(ct);

            return new PaginationResponse<ProductResponse>(
                page, 
                pageSize, 
                totalCount,
                products);

        }

        public async Task<ProductResponse?> GetProductByIdAsync(Guid productId, CancellationToken ct)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == productId)
                .Select(p => new ProductResponse(
                    p.Id, 
                    p.Name, 
                    p.Description,
                    p.CreatedAt))
                .FirstOrDefaultAsync(ct);
        }

        public async Task<bool> IsProductNameExistAsync(string productName, CancellationToken ct)
        {
            return await _context.Products
                .AsNoTracking()
                .AnyAsync(p => 
                p.Name == productName, ct);
        }
    }
}
