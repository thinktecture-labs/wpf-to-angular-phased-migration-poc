using System;
using System.Threading.Tasks;

namespace WpfApp.DeleteContactDialog;

public interface IDeleteContactSession : IDisposable
{
    Task DeleteContactAsync(Guid contactId);
}