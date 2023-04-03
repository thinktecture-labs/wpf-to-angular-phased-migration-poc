using System.Windows.Controls;

namespace WpfApp.ComponentSampleForm;

public sealed partial class ComponentSampleFormView : UserControl
{
    public ComponentSampleFormView() => InitializeComponent();

    public ComponentSampleFormView(ComponentSampleFormViewModel viewModel) : this() => DataContext = viewModel;
}