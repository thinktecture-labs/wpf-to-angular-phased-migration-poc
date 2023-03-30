using System.Threading;
using System.Threading.Tasks;
using Backend.Shared;
using Light.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Backend.Contacts.GetContacts;

public static class GetContactsEndpoint
{
    public static WebApplication MapGetContacts(this WebApplication app)
    {
        app.MapGet("/api/contacts", GetContacts);
        return app;
    }
    
    public static async Task<IResult> GetContacts(ValidationContext validationContext,
                                                  IGetContactsUnitOfWork unitOfWork,
                                                  int skip = 0,
                                                  int take = 30,
                                                  string? searchTerm = null,
                                                  CancellationToken cancellationToken = default)
    {
        if (validationContext.CheckPagingParametersForErrors(skip, take, out var errors))
            return Results.BadRequest(errors);

        var contacts = await unitOfWork.GetContactsAsync(skip, take, searchTerm, cancellationToken);
        return Results.Ok(contacts);
    }
}