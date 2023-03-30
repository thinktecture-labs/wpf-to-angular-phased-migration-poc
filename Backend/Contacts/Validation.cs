using System;
using Backend.DataAccess;
using Light.Validation;
using Light.Validation.Checks;
using Range = Light.Validation.Tools.Range;

namespace Backend.Contacts;

public static class Validation
{
    public static void ValidateContactProperties<T>(this ValidationContext context, T dto)
        where T : IContactProperties
    {
        dto.FirstName = context.Check(dto.FirstName).HasLengthIn(Range.FromInclusive(1).ToInclusive(200));
        dto.LastName = context.Check(dto.LastName).IsNotNull();
        dto.Email = context.Check(dto.Email).IsEmail();

        var now = DateOnly.FromDateTime(DateTime.Today);
        var lowerLimit = new DateOnly(1900, 1, 1);
        var upperLimit = now.AddYears(-18);
        context.Check(dto.DateOfBirth).IsIn(Range.FromInclusive(lowerLimit).ToInclusive(upperLimit));
    }
}