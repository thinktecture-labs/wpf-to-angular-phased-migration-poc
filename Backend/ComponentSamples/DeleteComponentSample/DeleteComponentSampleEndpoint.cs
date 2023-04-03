using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Backend.ComponentSamples.DeleteComponentSample;

public static class DeleteComponentSampleEndpoint
{
    public static WebApplication MapDeleteComponentSample(this WebApplication app)
    {
        app.MapDelete("/api/componentSamples/{id:guid}", DeleteComponentSample);
        return app;
    }

    public static async Task<IResult> DeleteComponentSample(IDeleteComponentSampleUnitOfWork unitOfWork,
                                                            ILogger logger,
                                                            Guid id,
                                                            CancellationToken cancellationToken = default)
    {
        var componentSample = await unitOfWork.GetComponentSampleAsync(id, cancellationToken);
        if (componentSample is null)
            return Results.NotFound();

        unitOfWork.RemoveComponentSample(componentSample);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.Information("Deleted {@ComponentSample}", componentSample);
        return Results.NoContent();
    }
}