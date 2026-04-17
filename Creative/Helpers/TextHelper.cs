namespace Creative.Helpers;

public static class TextHelper
{
    public static string Truncate(string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            return text;

        return text[..maxLength] + "...";
    }

    public static int WordCount(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        return text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public static string Capitalize(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return char.ToUpper(text[0]) + text[1..].ToLower();
    }
}
