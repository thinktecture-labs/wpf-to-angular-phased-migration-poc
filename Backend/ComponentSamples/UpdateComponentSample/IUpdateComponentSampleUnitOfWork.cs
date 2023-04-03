using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.ComponentSamples.UpdateComponentSample;

public interface IUpdateComponentSampleUnitOfWork : IUnitOfWork
{
    Task<ComponentSample?> GetComponentSampleAsync(Guid componentSampleId, CancellationToken cancellationToken = default);
}