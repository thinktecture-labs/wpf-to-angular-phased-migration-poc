using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.ComponentSamples.GetComponentSample;

public sealed class InMemoryComponentSampleUnitOfWork : IGetComponentSampleUnitOfWork
{
    public InMemoryComponentSampleUnitOfWork(ComponentSampleContext context) => Context = context;

    private ComponentSampleContext Context { get; }

    public Task<ComponentSample?> GetComponentSampleAsync(Guid componentSampleId,
                                                          CancellationToken cancellationToken = default) =>
        Context.ComponentSamplesLookup.TryGetValue(componentSampleId, out var componentSample) ?
            Task.FromResult<ComponentSample?>(componentSample) :
            Task.FromResult<ComponentSample?>(null);
}