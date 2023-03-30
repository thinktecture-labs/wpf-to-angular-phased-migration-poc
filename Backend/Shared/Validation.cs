using System.Diagnostics.CodeAnalysis;
using Light.Validation;
using Light.Validation.Checks;
using Light.Validation.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Shared;

public static class Validation
{
    public static IServiceCollection AddLightValidation(this IServiceCollection services)
    {
        return services.AddSingleton<IValidationContextFactory>(ValidationContextFactory.Instance)
                       .AddTransient(_ => ValidationContextFactory.CreateContext());
    }
    
    public static bool CheckPagingParametersForErrors(this ValidationContext context, int skip, int take, [NotNullWhen(true)] out object? errors)
    {
        context.Check(skip).IsGreaterThanOrEqualTo(0);
        context.Check(take).IsIn(Range.FromInclusive(1).ToInclusive(100));
        return context.TryGetErrors(out errors);
    }
}