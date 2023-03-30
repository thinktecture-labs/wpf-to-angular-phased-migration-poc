using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Backend.Contacts.DeleteContact;

public static class DeleteContactEndpoint
{
    public static WebApplication MapDeleteContact(this WebApplication app)
    {
        app.MapDelete("/api/contacts/{id}", DeleteContact);
        return app;
    }

    public static async Task<IResult> DeleteContact(IDeleteContactUnitOfWork unitOfWork,
                                                    ILogger logger,
                                                    Guid id,
                                                    CancellationToken cancellationToken = default)
    {
        var contact = await unitOfWork.GetContactAsync(id, cancellationToken);
        if (contact is null)
            return Results.NotFound();

        unitOfWork.RemoveContact(contact);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.Information("Deleted {@Contact}", contact);
        return Results.NoContent();
    }
}