using System;
using System.Net.Http;
using System.Threading.Tasks;
using WpfApp.Http;

namespace WpfApp.DeleteComponentSampleDialog;

public sealed class HttpDeleteSampleSession : BaseHttpSession, IDeleteSampleSession
{
    public HttpDeleteSampleSession(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

    public async Task DeleteSampleAsync(Guid componentSampleId)
    {
        var relativeUrl = "/api/componentSamples/" + componentSampleId;
        using var response = await HttpClient.DeleteAsync(relativeUrl);
        response.EnsureSuccessStatusCode();
    }
}