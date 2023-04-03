using System.Threading.Tasks;

namespace WpfApp.EndlessScrolling;

public interface IPagingViewModel
{
    Task LoadNextPageAsync();
}