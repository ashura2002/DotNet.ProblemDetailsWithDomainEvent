using Domain.Events;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Product : Aggregate
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }


        private Product(
            string name,
            string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Product Create(
            string name,
            string description)
        {
            name = ValidateName(name);
            var product = new Product(name, description);
            product.RaiseEvent(new ProductCreatedDomainEvent(product.Id));
            return product;
        }


        public void UpdateProductName(string newProductName)
        {
            if (Name == newProductName)
                return;

            newProductName = ValidateName(newProductName);
            Name = newProductName;
            Touch();
        }


        public void Touch()
        {
            UpdatedAt = DateTime.UtcNow;
        }


        private static string ValidateName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainRuleException("Product name cannot be empty.");

            value = value.Trim();
            if (value.Length < 3)
                throw new DomainRuleException("Product name must contain 3 or more characters length.");

            return value;
        }
    }
}
