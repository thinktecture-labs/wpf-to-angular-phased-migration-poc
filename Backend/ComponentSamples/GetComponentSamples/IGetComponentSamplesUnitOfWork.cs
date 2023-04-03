using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.ComponentSamples.GetComponentSamples;

public interface IGetComponentSamplesUnitOfWork
{
    Task<List<ComponentSample>> GetComponentSamplesAsync(int skip, int take, string? searchTerm, CancellationToken cancellationToken = default);
}