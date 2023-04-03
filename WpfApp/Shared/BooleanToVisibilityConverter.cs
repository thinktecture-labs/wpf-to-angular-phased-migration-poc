using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp.Shared;

public sealed class BooleanToVisibilityConverter : IValueConverter
{
    public Visibility VisibilityWhenFalseOrNull { get; set; } = Visibility.Collapsed;
    public Visibility VisibilityWhenTrue { get; set; } = Visibility.Visible;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is true ? VisibilityWhenTrue : VisibilityWhenFalseOrNull;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        value is Visibility.Visible;
}