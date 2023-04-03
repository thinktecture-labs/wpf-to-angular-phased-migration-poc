using Light.GuardClauses;
using WpfApp.EndlessScrolling;

namespace WpfApp.ComponentSampleList;

public readonly record struct SampleListFilters(string SearchTerm) : IPagingFilters
{
    public bool AreNoFiltersApplied => SearchTerm.IsNullOrWhiteSpace();
}