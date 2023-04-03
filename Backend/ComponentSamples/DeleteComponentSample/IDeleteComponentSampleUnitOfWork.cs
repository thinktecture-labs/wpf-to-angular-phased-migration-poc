using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataAccess;

namespace Backend.ComponentSamples.DeleteComponentSample;

public interface IDeleteComponentSampleUnitOfWork : IUnitOfWork
{
    Task<ComponentSample?> GetComponentSampleAsync(Guid id, CancellationToken cancellationToken = default);
    void RemoveComponentSample(ComponentSample componentSample);
}