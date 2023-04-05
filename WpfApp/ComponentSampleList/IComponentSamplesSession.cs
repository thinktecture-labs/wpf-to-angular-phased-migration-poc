using System;
using System.Threading.Tasks;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleList;

public interface IComponentSamplesSession : IDisposable
{
    Task<ComponentSample?> GetComponentSampleAsync(Guid sampleId);
}