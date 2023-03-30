using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Backend.Contacts.CreateContact;

public static class CreateContactEndpoint
{
    public static WebApplication MapCreateContact(this WebApplication app)
    {
        app.MapPost("/api/contacts", CreateContact);
        return app;
    }

    public static async Task<IResult> CreateContact(CreateContactDtoValidator validator,
                                                    ICreateContactUnitOfWork unitOfWork,
                                                    ILogger logger,
                                                    CreateContactDto dto,
                                                    CancellationToken cancellationToken = default)
    {
        if (validator.CheckForErrors(dto, out var errors))
            return Results.BadRequest(errors);

        var newContact = dto.ToContact();
        unitOfWork.AddContact(newContact);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.Information("Created {@Contact}", newContact);
        return Results.Created("/api/contacts/" + newContact.Id, newContact);
    }
}