using Backend.DataAccess;
using Light.Validation;
using Light.Validation.Checks;

namespace Backend.ComponentSamples.UpdateComponentSample;

public sealed class UpdateComponentSampleValidator : Validator<ComponentSample>
{
    public UpdateComponentSampleValidator(IValidationContextFactory validationContextFactory) : base(validationContextFactory) { }
    
    protected override ComponentSample PerformValidation(ValidationContext context, ComponentSample dto)
    {
        context.Check(dto.Id).IsNotEmpty();
        context.ValidateComponentSampleProperties(dto);
        return dto;
    }
}