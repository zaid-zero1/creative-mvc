namespace Creative.Helpers;

public record TextStats(
    int WordCount,
    int SentenceCount,
    int ParagraphCount,
    string LongestWord,
    double AverageWordLength);

public static class TextAnalyzer
{
    private static readonly HashSet<string> StopWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "the", "a", "an", "is", "in", "on", "at", "to", "and", "or", "but", "of", "for", "it", "this", "that"
    };

    public static string GetReadingLevel(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return "Unknown";

        var sentences = text.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
        var words = ExtractWords(text);

        if (words.Count == 0)
            return "Unknown";

        double avgWordLength = words.Average(w => (double)w.Length);
        double avgSentenceLength = sentences.Length == 0
            ? words.Count
            : words.Count / (double)sentences.Length;

        if (avgWordLength >= 7 || avgSentenceLength >= 25)
            return "Hard";

        if (avgWordLength <= 4 && avgSentenceLength <= 10)
            return "Easy";

        return "Medium";
    }

    public static List<string> ExtractKeywords(string text, int topN)
    {
        if (string.IsNullOrWhiteSpace(text) || topN <= 0)
            return new List<string>();

        return ExtractWords(text)
            .Select(w => w.ToLowerInvariant())
            .Where(w => !StopWords.Contains(w))
            .GroupBy(w => w)
            .OrderByDescending(g => g.Count())
            .Take(topN)
            .Select(g => g.Key)
            .ToList();
    }

    public static TextStats SummarizeStats(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new TextStats(0, 0, 0, string.Empty, 0.0);

        var words = ExtractWords(text);
        int sentenceCount = text.Count(c => c == '.' || c == '!' || c == '?');
        var paragraphs = text.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries)
                             .Where(p => !string.IsNullOrWhiteSpace(p))
                             .ToList();

        string longestWord = words.Count == 0
            ? string.Empty
            : words.OrderByDescending(w => w.Length).First();

        double avgWordLength = words.Count == 0
            ? 0.0
            : Math.Round(words.Average(w => (double)w.Length), 1);

        return new TextStats(
            WordCount: words.Count,
            SentenceCount: sentenceCount,
            ParagraphCount: paragraphs.Count,
            LongestWord: longestWord,
            AverageWordLength: avgWordLength);
    }

    public static bool ContainsRepeatedWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        var words = ExtractWords(text);

        for (int i = 1; i < words.Count; i++)
        {
            if (string.Equals(words[i], words[i - 1], StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private static List<string> ExtractWords(string text)
    {
        return text.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(w => new string(w.Where(char.IsLetterOrDigit).ToArray()))
                   .Where(w => w.Length > 0)
                   .ToList();
    }
}
