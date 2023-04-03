using System;
using System.Threading.Tasks;

namespace WpfApp.DeleteComponentSampleDialog;

public interface IDeleteSampleSession : IDisposable
{
    Task DeleteSampleAsync(Guid componentSampleId);
}