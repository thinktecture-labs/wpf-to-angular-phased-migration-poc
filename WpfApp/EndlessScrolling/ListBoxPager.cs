using System;
using System.Windows.Controls;
using Light.GuardClauses;
using WpfApp.Shared;
using Range = Light.GuardClauses.Range;

namespace WpfApp.EndlessScrolling;

public sealed class ListBoxPager<TViewModel>
    where TViewModel : IHasPagingViewModel
{
    public ListBoxPager(UserControl view, ScrollViewer scrollViewer, double thresholdPercentage)
    {
        View = view;
        ScrollViewer = scrollViewer;
        ThresholdPercentage = thresholdPercentage.MustBeIn(Range.FromExclusive(0.0).ToInclusive(1.0));
    }

    private UserControl View { get; }

    private ScrollViewer ScrollViewer { get; }

    private double ThresholdPercentage { get; }

    private void LoadNextPageWhenScrollViewerHitsThreshold()
    {
        ScrollViewer.ScrollChanged += ScrollViewerOnScrollChanged;
    }

    private void ScrollViewerOnScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (View.DataContext is not TViewModel viewModel)
            return;

        if (Paging.CheckIfScrollIsNearTheEnd(e.VerticalOffset,
                                             e.VerticalChange,
                                             e.ViewportHeight,
                                             e.ExtentHeight,
                                             ThresholdPercentage))
        {
            viewModel.PagingViewModel.LoadNextPageAsync();
        }
    }


    public static void EnableEndlessScrolling(UserControl view, ListBox listBox, double thresholdPercentage = 0.9)
    {
        if (view.DataContext is not TViewModel)
            return;
        
        var scrollViewer = listBox.FindFirstVisualChildOfType<ScrollViewer>();
        if (scrollViewer is null)
            throw new InvalidOperationException("Could not find scroll viewer child of list box");
        
        new ListBoxPager<TViewModel>(view, scrollViewer, thresholdPercentage)
           .LoadNextPageWhenScrollViewerHitsThreshold();
    }
    
}