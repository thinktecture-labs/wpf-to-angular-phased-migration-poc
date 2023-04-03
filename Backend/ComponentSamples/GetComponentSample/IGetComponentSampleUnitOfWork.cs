using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.ComponentSamples.GetComponentSample;

public interface IGetComponentSampleUnitOfWork
{
    Task<ComponentSample?> GetComponentSampleAsync(Guid componentSampleId,
                                                   CancellationToken cancellationToken = default);
}