using System.Windows.Controls;

namespace WpfApp.ContactForm;

public sealed partial class ContactFormView : UserControl
{
    public ContactFormView() => InitializeComponent();

    public ContactFormView(ContactFormViewModel viewModel) : this() => DataContext = viewModel;
}