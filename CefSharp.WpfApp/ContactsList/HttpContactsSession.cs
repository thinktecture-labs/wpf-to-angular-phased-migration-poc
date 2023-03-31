﻿using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CefSharp.WpfApp.Http;
using CefSharp.WpfApp.Shared;
using Light.GuardClauses;

namespace CefSharp.WpfApp.ContactsList;

public sealed class HttpContactsSession : BaseHttpSession, IContactsSession
{
    public HttpContactsSession(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }
    
    public Task<List<Contact>> GetItemsAsync(ContactListFilters filters, int skip, int take, CancellationToken cancellationToken)
    {
        var queryParameters = HttpUtility.ParseQueryString(string.Empty);
        queryParameters.Add(nameof(skip), skip.ToString());
        queryParameters.Add(nameof(take), take.ToString());
        var searchTerm = filters.SearchTerm;
        if (!searchTerm.IsNullOrWhiteSpace())
            queryParameters.Add(nameof(searchTerm), searchTerm);

        var relativeUrl = "/api/contacts?" + queryParameters;
        return HttpClient.GetFromJsonAsync<List<Contact>>(relativeUrl, JsonOptions, cancellationToken)!;
    }
}