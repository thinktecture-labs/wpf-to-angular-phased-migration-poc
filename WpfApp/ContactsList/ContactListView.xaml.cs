using System.Windows;
using System.Windows.Controls;
using WpfApp.EndlessScrolling;

namespace WpfApp.ContactsList;

// ReSharper disable once RedundantExtendsListEntry
public sealed partial class ContactListView : UserControl
{
    public ContactListView() => InitializeComponent();

    public ContactListView(ContactListViewModel viewModel) : this()
    {
        DataContext = viewModel;
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e) =>
        ListBoxPager<ContactListViewModel>.EnableEndlessScrolling(this, ListBox);
}