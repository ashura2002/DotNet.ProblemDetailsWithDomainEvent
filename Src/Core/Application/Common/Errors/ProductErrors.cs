
using Domain.Enums;

namespace Application.Common.Errors
{
    public static class ProductErrors
    {
        public static readonly Error ProductNameExisted = new("PRODUCT.CONFLICT", "Product name is already existed", ErrorType.Conflict);
        public static readonly Error ProductNotFound = new("PRODUCT.NOT_FOUND", "Product not found", ErrorType.NotFound);
    }
}
