using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.ComponentSamples.CreateComponentSample;

public sealed class InMemoryCreateComponentSampleUnitOfWork : ICreateComponentSampleUnitOfWork
{
    public InMemoryCreateComponentSampleUnitOfWork(ComponentSampleContext context) => Context = context;

    private ComponentSampleContext Context { get; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Context.SaveChanges();
        return Task.CompletedTask;
    }

    public void AddComponentSample(ComponentSample componentSample) => Context.AddComponentSample(componentSample);
}