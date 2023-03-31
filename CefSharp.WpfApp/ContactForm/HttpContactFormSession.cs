using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CefSharp.WpfApp.Http;
using CefSharp.WpfApp.Shared;

namespace CefSharp.WpfApp.ContactForm;

public sealed class HttpContactFormSession : BaseHttpSession, IContactFormSession
{
    public HttpContactFormSession(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

    public async Task CreateContactAsync(Contact contact)
    {
        using var response = await HttpClient.PostAsJsonAsync("/api/contacts", contact, JsonOptions);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateContactAsync(Contact contact)
    {
        using var response = await HttpClient.PutAsJsonAsync("/api/contacts", contact, JsonOptions);
        response.EnsureSuccessStatusCode();
    }
}