using Application.Common.Errors;
using Application.Common.Results;
using Application.Interfaces.Products;
using Application.Interfaces.Uof;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResultWithReturnValue<Guid>>
    {

        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IUnitOfWork unitOfWork)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<ResultWithReturnValue<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var isProductNameExist = await _productReadRepository.IsProductNameExistAsync(request.Name, cancellationToken);
            if (isProductNameExist)
                return ResultWithReturnValue<Guid>.Failure(ProductErrors.ProductNameExisted);

            var product = Product.Create(request.Name, request.Description);
            _productWriteRepository.Add(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return ResultWithReturnValue<Guid>.Success(product.Id);
        }
    }
}
