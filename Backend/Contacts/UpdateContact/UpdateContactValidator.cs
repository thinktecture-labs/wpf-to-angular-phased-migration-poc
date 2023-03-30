using Backend.DataAccess;
using Light.Validation;
using Light.Validation.Checks;

namespace Backend.Contacts.UpdateContact;

public sealed class UpdateContactValidator : Validator<Contact>
{
    public UpdateContactValidator(IValidationContextFactory validationContextFactory) : base(validationContextFactory) { }
    
    protected override Contact PerformValidation(ValidationContext context, Contact dto)
    {
        context.Check(dto.Id).IsNotEmpty();
        context.ValidateContactProperties(dto);
        return dto;
    }
}