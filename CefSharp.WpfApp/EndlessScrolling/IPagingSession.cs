using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CefSharp.WpfApp.EndlessScrolling;

public interface IPagingSession<TItem, in TFilters> : IAsyncDisposable, IDisposable
{
    Task<List<TItem>> GetItemsAsync(TFilters searchTerm, int skip, int take, CancellationToken cancellationToken);
}