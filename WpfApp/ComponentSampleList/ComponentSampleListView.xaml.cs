using System.Windows.Controls;

namespace WpfApp.ComponentSampleList;

// ReSharper disable once RedundantExtendsListEntry
public sealed partial class ComponentSampleListView : UserControl
{
    public ComponentSampleListView() => InitializeComponent();

    public ComponentSampleListView(ComponentSampleListViewModel viewModel) : this()
    {
        DataContext = viewModel;
        viewModel.BoundObject.Dispatcher = Dispatcher;
        ChromiumWebBrowser.JavascriptObjectRepository.ResolveObject += (_, e) =>
        {
            const string boundObjectName = "samplesListBoundObject";
            if (e.ObjectName == boundObjectName)
                e.ObjectRepository.Register(boundObjectName, viewModel.BoundObject);
        };
    }
}