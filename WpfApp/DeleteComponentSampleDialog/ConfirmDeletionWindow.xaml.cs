using System.Windows;
using MahApps.Metro.Controls;

namespace WpfApp.DeleteComponentSampleDialog;

// ReSharper disable once RedundantExtendsListEntry
public sealed partial class ConfirmDeletionWindow : MetroWindow
{
    public ConfirmDeletionWindow() => InitializeComponent();

    public ConfirmDeletionWindow(ConfirmDeletionViewModel viewModel) : this() => DataContext = viewModel;

    private void OnCancelClicked(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    private async void OnDeleteClicked(object sender, RoutedEventArgs e)
    {
        if (DataContext is not ConfirmDeletionViewModel viewModel)
            return;

        DialogResult = await viewModel.DeleteSampleAsync();
        Close();
    }
}