using System.Threading.Tasks;

namespace CefSharp.WpfApp.EndlessScrolling;

public interface IPagingViewModel
{
    Task LoadNextPageAsync();
}