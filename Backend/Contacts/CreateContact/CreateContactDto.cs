using System;
using Backend.DataAccess;

namespace Backend.Contacts.CreateContact;

public sealed class CreateContactDto : IContactProperties
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }

    public Contact ToContact() => new ()
    {
        Id = Guid.NewGuid(),
        FirstName = FirstName,
        LastName = LastName,
        Email = Email,
        DateOfBirth = DateOfBirth
    };
}