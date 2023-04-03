using System.Windows;
using System.Windows.Controls;
using WpfApp.EndlessScrolling;

namespace WpfApp.ComponentSampleList;

// ReSharper disable once RedundantExtendsListEntry
public sealed partial class ComponentSampleListView : UserControl
{
    public ComponentSampleListView() => InitializeComponent();

    public ComponentSampleListView(ComponentSampleListViewModel viewModel) : this()
    {
        DataContext = viewModel;
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e) =>
        ListBoxPager<ComponentSampleListViewModel>.EnableEndlessScrolling(this, ListBox);
}