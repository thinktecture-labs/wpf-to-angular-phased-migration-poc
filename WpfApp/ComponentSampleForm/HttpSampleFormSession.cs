using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WpfApp.Http;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleForm;

public sealed class HttpSampleFormSession : BaseHttpSession, ISampleFormSession
{
    public HttpSampleFormSession(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

    public async Task CreateComponentSampleAsync(ComponentSample componentSample)
    {
        using var response = await HttpClient.PostAsJsonAsync("/api/componentSamples", componentSample, JsonOptions);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateComponentSampleAsync(ComponentSample componentSample)
    {
        using var response = await HttpClient.PutAsJsonAsync("/api/componentSamples", componentSample, JsonOptions);
        response.EnsureSuccessStatusCode();
    }
}