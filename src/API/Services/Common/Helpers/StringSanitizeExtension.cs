using Ganss.Xss;

namespace Common.Helpers;

/// <summary>
/// Usage: str = str.Sanitize()
/// </summary>
public static class StringSanitizeExtension
{
    public static string Sanitize(this string s)
    {
        var sanitizer = new HtmlSanitizer();
        var san = sanitizer.Sanitize(s);
        return san;
    }
}
