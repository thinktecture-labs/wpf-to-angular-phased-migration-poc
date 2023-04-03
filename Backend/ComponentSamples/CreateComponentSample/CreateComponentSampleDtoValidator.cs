using Light.Validation;

namespace Backend.ComponentSamples.CreateComponentSample;

public sealed class CreateComponentSampleDtoValidator : Validator<CreateComponentSampleDto>
{
    public CreateComponentSampleDtoValidator(IValidationContextFactory validationContextFactory)
        : base(validationContextFactory) { }
    
    protected override CreateComponentSampleDto PerformValidation(ValidationContext context,
                                                                  CreateComponentSampleDto dto)
    {
        context.ValidateComponentSampleProperties(dto);
        return dto;
    }
}