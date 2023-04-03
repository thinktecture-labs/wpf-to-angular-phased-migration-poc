using System;
using System.Threading.Tasks;
using Serilog;
using WpfApp.Shared;

namespace WpfApp.DeleteContactDialog;

public sealed class ConfirmDeletionViewModel
{
    public ConfirmDeletionViewModel(Contact contact,
                                    Func<IDeleteContactSession> createSession,
                                    ILogger logger)
    {
        Contact = contact;
        CreateSession = createSession;
        Logger = logger;
    }
    
    public Contact Contact { get; }
    private Func<IDeleteContactSession> CreateSession { get; }
    private ILogger Logger { get; }

    public async Task<bool> DeleteContactAsync()
    {
        try
        {
            using var session = CreateSession();
            await session.DeleteContactAsync(Contact.Id);
            return true;
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Could not delete contact");
            return false;
        }
    }
}