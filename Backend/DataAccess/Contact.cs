using System;

namespace Backend.DataAccess;

public sealed class Contact : IContactProperties
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }

    public void CopyValuesTo(Contact other)
    {
        other.FirstName = FirstName;
        other.LastName = LastName;
        other.Email = Email;
        other.DateOfBirth = DateOfBirth;
    }
}

public interface IContactProperties
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    DateOnly DateOfBirth { get; set; }
}