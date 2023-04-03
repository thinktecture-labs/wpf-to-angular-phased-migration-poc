using System;
using System.Net.Http;
using System.Threading.Tasks;
using WpfApp.Http;

namespace WpfApp.DeleteContactDialog;

public sealed class HttpDeleteContactSession : BaseHttpSession, IDeleteContactSession
{
    public HttpDeleteContactSession(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

    public async Task DeleteContactAsync(Guid contactId)
    {
        var relativeUrl = "/api/contacts/" + contactId;
        using var response = await HttpClient.DeleteAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
    }
}