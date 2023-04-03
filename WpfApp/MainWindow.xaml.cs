using MahApps.Metro.Controls;

namespace WpfApp;

public sealed partial class MainWindow : MetroWindow
{
    public MainWindow() => InitializeComponent();

    public MainWindow(MainWindowViewModel viewModel) : this() => DataContext = viewModel;
}