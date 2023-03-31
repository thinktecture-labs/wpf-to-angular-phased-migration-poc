using System.Text.Json;

namespace CefSharp.WpfApp.Http;

public static class Json
{
    public static JsonSerializerOptions DefaultOptions { get; } =
        new () { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
}