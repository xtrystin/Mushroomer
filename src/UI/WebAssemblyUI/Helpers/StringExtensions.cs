using Ganss.Xss;

namespace WebAssemblyUI.Helpers;

/// <summary>
/// Usage: str = str.Sanitize()
/// </summary>
public static class StringExtensions
{
    public static string Sanitize(this string s)
    {
        var sanitizer = new HtmlSanitizer();
        var san = sanitizer.Sanitize(s);
        return san;
    }
}
