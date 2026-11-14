using Application.Features.Products.Commands;
using FluentValidation;

namespace Application.Features.Products.Validators
{
    public sealed class UpdateProductNameCommandValidator : AbstractValidator<UpdateProductNameCommand>
    {
        public UpdateProductNameCommandValidator()
        {
            RuleFor(x => x.NewProductName)
            .NotEmpty()
            .MinimumLength(3)
            .WithMessage("Invalid product name length.");
        }
    }
}
