using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Light.GuardClauses;
using WpfApp.Http;
using WpfApp.Shared;

namespace WpfApp.ComponentSampleList;

public sealed class HttpComponentSamplesSession : BaseHttpSession, IComponentSamplesSession
{
    public HttpComponentSamplesSession(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }
    
    public Task<List<ComponentSample>> GetItemsAsync(SampleListFilters filters, int skip, int take, CancellationToken cancellationToken)
    {
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters.Add(nameof(skip), skip.ToString());
        queryParameters.Add(nameof(take), take.ToString());
        var searchTerm = filters.SearchTerm;
        if (!searchTerm.IsNullOrWhiteSpace())
            queryParameters.Add(nameof(searchTerm), searchTerm);

        var relativeUrl = "/api/componentSamples?" + queryParameters;
        return HttpClient.GetFromJsonAsync<List<ComponentSample>>(relativeUrl, JsonOptions, cancellationToken)!;
    }
}