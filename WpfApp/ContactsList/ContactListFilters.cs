using Light.GuardClauses;
using WpfApp.EndlessScrolling;

namespace WpfApp.ContactsList;

public readonly record struct ContactListFilters(string SearchTerm) : IPagingFilters
{
    public bool AreNoFiltersApplied => SearchTerm.IsNullOrWhiteSpace();
}