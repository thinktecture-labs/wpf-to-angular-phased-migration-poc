using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Backend.ComponentSamples.CreateComponentSample;

public static class CreateComponentSampleEndpoint
{
    public static WebApplication MapCreateComponentSample(this WebApplication app)
    {
        app.MapPost("/api/componentSamples", CreateComponentSample);
        return app;
    }

    public static async Task<IResult> CreateComponentSample(CreateComponentSampleDtoValidator validator,
                                                            ICreateComponentSampleUnitOfWork unitOfWork,
                                                            ILogger logger,
                                                            CreateComponentSampleDto dto,
                                                            CancellationToken cancellationToken = default)
    {
        if (validator.CheckForErrors(dto, out var errors))
            return Results.BadRequest(errors);

        var newComponentSample = dto.ToComponentSample();
        unitOfWork.AddComponentSample(newComponentSample);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.Information("Created {@ComponentSample}", newComponentSample);
        return Results.Created("/api/componentSamples/" + newComponentSample.Id, newComponentSample);
    }
}