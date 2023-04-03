using System;
using WpfApp.Shared;

namespace WpfApp.ContactForm;

public sealed class NavigateToContactFormCommand : INavigateToContactFormCommand
{
    public NavigateToContactFormCommand(Func<Contact, ContactFormViewModel> createContactFormViewModel,
                                        INavigator navigator)
    {
        CreateContactFormViewModel = createContactFormViewModel;
        Navigator = navigator;
    }

    private Func<Contact, ContactFormViewModel> CreateContactFormViewModel { get; }
    private INavigator Navigator { get; }

    public void Navigate(Contact contact)
    {
        var viewModel = CreateContactFormViewModel(contact);
        var view = new ContactFormView(viewModel);
        Navigator.Show(view);
    }
}