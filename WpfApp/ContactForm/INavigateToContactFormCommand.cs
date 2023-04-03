using WpfApp.Shared;

namespace WpfApp.ContactForm;

public interface INavigateToContactFormCommand
{
    void Navigate(Contact contact);
}