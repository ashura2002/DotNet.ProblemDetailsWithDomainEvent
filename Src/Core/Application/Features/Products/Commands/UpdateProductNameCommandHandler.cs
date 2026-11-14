using Application.Common.Errors;
using Application.Common.Results;
using Application.Interfaces.Products;
using Application.Interfaces.Uof;
using MediatR;

namespace Application.Features.Products.Commands
{
    public sealed class UpdateProductNameCommandHandler : IRequestHandler<UpdateProductNameCommand, Result>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductNameCommandHandler(IProductWriteRepository productWriteRepository, IUnitOfWork unitOfWork)
        {
            _productWriteRepository = productWriteRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result> Handle(UpdateProductNameCommand request, CancellationToken cancellationToken)
        {
            var product = await _productWriteRepository.GetProductByIdAsync(request.ProductId, cancellationToken);

            if (product is null)
                return Result.Failure(ProductErrors.ProductNotFound);

            product.UpdateProductName(request.NewProductName);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
