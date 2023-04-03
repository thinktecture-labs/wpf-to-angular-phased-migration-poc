using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp.Http;

public abstract class BaseHttpSession : IAsyncDisposable, IDisposable
{
    protected BaseHttpSession(IHttpClientFactory httpClientFactory, JsonSerializerOptions? options = null)
    {
        HttpClient = httpClientFactory.CreateClient(HttpConstants.BackendHttpClientName);
        JsonOptions = options ?? Json.DefaultOptions;
    }
    
    protected HttpClient HttpClient { get; }
    protected JsonSerializerOptions JsonOptions { get; }
    
    public ValueTask DisposeAsync()
    {
        HttpClient.Dispose();
        return default;
    }

    public void Dispose() => HttpClient.Dispose();
}