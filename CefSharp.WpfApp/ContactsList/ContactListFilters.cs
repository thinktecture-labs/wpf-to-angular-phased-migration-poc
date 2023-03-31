using CefSharp.WpfApp.EndlessScrolling;
using Light.GuardClauses;

namespace CefSharp.WpfApp.ContactsList;

public readonly record struct ContactListFilters(string SearchTerm) : IPagingFilters
{
    public bool AreNoFiltersApplied => SearchTerm.IsNullOrWhiteSpace();
}