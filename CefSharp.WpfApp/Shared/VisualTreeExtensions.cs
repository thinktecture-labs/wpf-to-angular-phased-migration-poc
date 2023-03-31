using System.Windows;
using System.Windows.Media;

namespace CefSharp.WpfApp.Shared;

public static class VisualTreeExtensions
{
    public static T? FindFirstVisualChildOfType<T>(this DependencyObject element)
        where T : DependencyObject
    {
        var numberOfChildren = VisualTreeHelper.GetChildrenCount(element);
        for (var i = 0; i < numberOfChildren; i++)
        {
            var child = VisualTreeHelper.GetChild(element, i);
            if (child is T target)
                return target;

            var childResult = FindFirstVisualChildOfType<T>(child);
            if (childResult is not null)
                return childResult;
        }

        return null;
    }
}