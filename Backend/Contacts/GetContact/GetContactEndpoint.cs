using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Backend.Contacts.GetContact;

public static class GetContactEndpoint
{
    public static WebApplication MapGetContact(this WebApplication app)
    {
        app.MapGet("/api/contacts/{id}", GetContact);
        return app;
    }

    public static async Task<IResult> GetContact(IGetContactUnitOfWork unitOfWork,
                                                 Guid id,
                                                 CancellationToken cancellationToken = default)
    {
        var contact = await unitOfWork.GetContactAsync(id, cancellationToken);
        return contact is null ?
            Results.NotFound() :
            Results.Ok(contact);
    }
}