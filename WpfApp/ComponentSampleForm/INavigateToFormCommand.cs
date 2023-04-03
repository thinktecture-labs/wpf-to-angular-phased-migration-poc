using WpfApp.Shared;

namespace WpfApp.ComponentSampleForm;

public interface INavigateToFormCommand
{
    void Navigate(ComponentSample componentSample);
}