using GymManagement.Application.Gyms.Commands.CreateGym; 
using MediatR;
using ErrorOr;
using FluentValidation;

namespace GymManagement.Application.Common.Behavior;

public class ValidationBehavior<TRequest, TResponse>
    (IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (validator != null)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var error1=validationResult
                    .Errors
                    .Select(error => Error.Validation(
                        code: error.PropertyName, 
                        description: error.ErrorMessage))
                    .ToList();
                var error = validationResult
                    .Errors
                    .ConvertAll(error =>
                        Error.Validation(
                            code: error.PropertyName,
                            description: error.ErrorMessage));
                var test = (dynamic)error;
                return test;
            }
        }

        return await next();
    }
}