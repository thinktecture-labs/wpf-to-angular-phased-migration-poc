using Backend.DataAccess;

namespace Backend.ComponentSamples.CreateComponentSample;

public interface ICreateComponentSampleUnitOfWork : IUnitOfWork
{
    void AddComponentSample(ComponentSample componentSample);
}