using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Backend.Contacts.UpdateContact;

public static class UpdateContactEndpoint
{
    public static WebApplication MapUpdateContact(this WebApplication app)
    {
        app.MapPut("/api/contacts", UpdateContact);
        return app;
    }

    public static async Task<IResult> UpdateContact(UpdateContactValidator validator,
                                                    IUpdateContactUnitOfWork unitOfWork,
                                                    ILogger logger,
                                                    Contact dto,
                                                    CancellationToken cancellationToken = default)
    {
        if (validator.CheckForErrors(dto, out var errors))
            return Results.BadRequest(errors);

        var contact = await unitOfWork.GetContactAsync(dto.Id, cancellationToken);
        if (contact is null)
            return Results.NotFound();

        dto.CopyValuesTo(contact);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.Information("Updated {@Contact}", contact);
        
        return Results.NoContent();
    }
}