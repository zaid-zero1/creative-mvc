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

    public static string Slugify(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        return text.Trim()
                   .ToLower()
                   .Replace(' ', '-')
                   .Replace("--", "-");
    }

    public static bool IsPalindrome(string text)
    {
        if (string.IsNullOrEmpty(text))
            return false;

        var cleaned = text.ToLower().Where(char.IsLetterOrDigit).ToArray();
        return cleaned.SequenceEqual(cleaned.Reverse());
    }

    public static string WrapText(string text, int lineWidth)
    {
        if (string.IsNullOrWhiteSpace(text) || lineWidth <= 0)
            return text ?? string.Empty;

        var words = text.Split(' ');
        var lines = new List<string>();
        var current = new System.Text.StringBuilder();

        foreach (var word in words)
        {
            int spaceNeeded = current.Length > 0 ? 1 : 0;

            if (current.Length + spaceNeeded + word.Length > lineWidth)
            {
                if (current.Length > 0)
                {
                    lines.Add(current.ToString());
                    current.Clear();
                }
            }

            if (current.Length > 0)
                current.Append(' ');

            current.Append(word);
        }

        if (current.Length > 0)
            lines.Add(current.ToString());

        return string.Join(Environment.NewLine, lines);
    }
}
