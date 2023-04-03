using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;
using FuzzySharp;

namespace Backend.ComponentSamples.GetComponentSamples;

public sealed class InMemoryGetComponentSamplesUnitOfWork : IGetComponentSamplesUnitOfWork
{
    public InMemoryGetComponentSamplesUnitOfWork(ComponentSampleContext context) => Context = context;

    private ComponentSampleContext Context { get; }

    public Task<List<ComponentSample>> GetComponentSamplesAsync(int skip,
                                                                int take,
                                                                string? searchTerm,
                                                                CancellationToken cancellationToken = default)
    {
        var componentSamples = string.IsNullOrWhiteSpace(searchTerm) ?
            Context.ComponentSamples :
            Process.ExtractSorted(new () { ComponentName = searchTerm },
                                  Context.ComponentSamples,
                                  c => c.ComponentName,
                                  cutoff: 60)
                   .Select(r => r.Value);

        return Task.FromResult(componentSamples.Skip(skip)
                                               .Take(take)
                                               .ToList());
    }
}