using System;
using WpfApp.Shared;

namespace WpfApp.ContactsList;

public sealed class NavigateToContactsListCommand : INavigateToContactsListCommand
{
    public NavigateToContactsListCommand(INavigator navigator,
                                         Func<ContactListViewModel> createContactListViewModel)
    {
        Navigator = navigator;
        CreateContactListViewModel = createContactListViewModel;
    }

    private INavigator Navigator { get; }
    private Func<ContactListViewModel> CreateContactListViewModel { get; }

    public async void Navigate()
    {
        var viewModel = CreateContactListViewModel();
        var view = new ContactListView(viewModel);
        Navigator.Show(view);
        await viewModel.PagingViewModel.LoadNextPageAsync();
    }
}