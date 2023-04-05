using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WpfApp.Http;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleList;

public sealed class HttpComponentSamplesSession : BaseHttpSession, IComponentSamplesSession
{
    public HttpComponentSamplesSession(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

    public Task<ComponentSample?> GetComponentSampleAsync(Guid sampleId)
    {
        var relativeUrl = "/api/componentSamples/" + sampleId;
        return HttpClient.GetFromJsonAsync<ComponentSample?>(relativeUrl, JsonOptions);
    }
}