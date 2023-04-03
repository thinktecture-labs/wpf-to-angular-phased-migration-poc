using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.ComponentSamples.UpdateComponentSample;

public sealed class InMemoryUpdateComponentSampleUnitOfWork : IUpdateComponentSampleUnitOfWork
{
    public InMemoryUpdateComponentSampleUnitOfWork(ComponentSampleContext context) => Context = context;

    private ComponentSampleContext Context { get; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<ComponentSample?> GetComponentSampleAsync(Guid componentSampleId,
                                                          CancellationToken cancellationToken = default) =>
        Context.ComponentSamplesLookup.TryGetValue(componentSampleId, out var componentSample) ?
            Task.FromResult<ComponentSample?>(componentSample) :
            Task.FromResult<ComponentSample?>(null);
}