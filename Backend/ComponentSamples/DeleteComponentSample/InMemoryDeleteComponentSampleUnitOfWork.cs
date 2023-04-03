using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.ComponentSamples.DeleteComponentSample;

public sealed class InMemoryDeleteComponentSampleUnitOfWork : IDeleteComponentSampleUnitOfWork
{
    public InMemoryDeleteComponentSampleUnitOfWork(ComponentSampleContext context) => Context = context;

    private ComponentSampleContext Context { get; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<ComponentSample?> GetComponentSampleAsync(Guid id, CancellationToken cancellationToken = default) =>
        Context.ComponentSamplesLookup.TryGetValue(id, out var componentSample) ?
            Task.FromResult<ComponentSample?>(componentSample) :
            Task.FromResult<ComponentSample?>(null);

    public void RemoveComponentSample(ComponentSample componentSample) => Context.RemoveComponentSample(componentSample);
}