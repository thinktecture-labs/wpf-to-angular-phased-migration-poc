using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Backend.ComponentSamples.GetComponentSample;

public static class GetComponentSampleEndpoint
{
    public static WebApplication MapGetComponentSample(this WebApplication app)
    {
        app.MapGet("/api/componentSamples/{id:guid}", GetComponentSample);
        return app;
    }

    public static async Task<IResult> GetComponentSample(IGetComponentSampleUnitOfWork unitOfWork,
                                                         Guid id,
                                                         CancellationToken cancellationToken = default)
    {
        var componentSample = await unitOfWork.GetComponentSampleAsync(id, cancellationToken);
        return componentSample is null ?
            Results.NotFound() :
            Results.Ok(componentSample);
    }
}