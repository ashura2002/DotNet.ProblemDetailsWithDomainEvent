using Application.Interfaces.Products;
using Domain.Entities;
using Infra.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public sealed class ProductWriteRepo : IProductWriteRepository
    {
        private readonly DemoDbContext _context;
        public ProductWriteRepo(DemoDbContext context)
        {
            _context = context;
        }


        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public async Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken ct)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId, ct);
        }

        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
