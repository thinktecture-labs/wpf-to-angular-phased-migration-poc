using System;
using System.Threading.Tasks;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleForm;

public interface ISampleFormSession : IDisposable
{
    Task CreateComponentSampleAsync(ComponentSample componentSample);
    Task UpdateComponentSampleAsync(ComponentSample componentSample);
}