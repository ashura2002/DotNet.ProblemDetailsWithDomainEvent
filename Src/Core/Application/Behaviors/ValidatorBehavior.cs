using FluentValidation;
using MediatR;

namespace Application.Behaviors
{
    public sealed class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!_validators.Any())
                return await next(cancellationToken);

            // create a validation context for the current request
            var validationContext = new ValidationContext<TRequest>(request);


            // flatten all the failure results into single list
            var validationResults = await Task.WhenAll(
                _validators.Select(v => 
                v.ValidateAsync(validationContext, cancellationToken)));

            // validate each result
            var validationErrors = validationResults.SelectMany(e => e.Errors)
            .Where(e => e is not null)
            .ToList();

            if(validationErrors.Count != 0)
                 throw new ValidationException(validationErrors);

            return await next(cancellationToken);
        }
    }
}