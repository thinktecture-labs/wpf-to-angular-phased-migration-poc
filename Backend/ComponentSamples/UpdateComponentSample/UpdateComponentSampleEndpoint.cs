using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Backend.ComponentSamples.UpdateComponentSample;

public static class UpdateComponentSampleEndpoint
{
    public static WebApplication MapUpdateComponentSample(this WebApplication app)
    {
        app.MapPut("/api/componentSamples", UpdateComponentSample);
        return app;
    }

    public static async Task<IResult> UpdateComponentSample(UpdateComponentSampleValidator validator,
                                                            IUpdateComponentSampleUnitOfWork unitOfWork,
                                                            ILogger logger,
                                                            ComponentSample dto,
                                                            CancellationToken cancellationToken = default)
    {
        if (validator.CheckForErrors(dto, out var errors))
            return Results.BadRequest(errors);

        var componentSample = await unitOfWork.GetComponentSampleAsync(dto.Id, cancellationToken);
        if (componentSample is null)
            return Results.NotFound();

        dto.CopyValuesTo(componentSample);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.Information("Updated {@ComponentSample}", componentSample);

        return Results.NoContent();
    }
}