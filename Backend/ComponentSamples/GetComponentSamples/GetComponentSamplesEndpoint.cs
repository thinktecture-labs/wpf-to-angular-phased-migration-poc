using System.Threading;
using System.Threading.Tasks;
using Backend.Shared;
using Light.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Backend.ComponentSamples.GetComponentSamples;

public static class GetComponentSamplesEndpoint
{
    public static WebApplication MapGetComponentSamples(this WebApplication app)
    {
        app.MapGet("/api/componentSamples", GetComponentSamples);
        return app;
    }

    public static async Task<IResult> GetComponentSamples(ValidationContext validationContext,
                                                          IGetComponentSamplesUnitOfWork unitOfWork,
                                                          int skip = 0,
                                                          int take = 30,
                                                          string? searchTerm = null,
                                                          CancellationToken cancellationToken = default)
    {
        if (validationContext.CheckPagingParametersForErrors(skip, take, out var errors))
            return Results.BadRequest(errors);

        var componentSamples = await unitOfWork.GetComponentSamplesAsync(skip, take, searchTerm, cancellationToken);
        return Results.Ok(componentSamples);
    }
}