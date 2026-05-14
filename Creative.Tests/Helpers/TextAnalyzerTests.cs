using Creative.Helpers;
using Xunit;

namespace Creative.Tests.Helpers;

public class TextAnalyzerTests
{
    #region GetReadingLevel Tests

    [Fact]
    public void GetReadingLevel_NullText_ReturnsUnknown()
    {
        string? text = null;

        var result = TextAnalyzer.GetReadingLevel(text!);

        Assert.Equal("Unknown", result);
    }

    [Fact]
    public void GetReadingLevel_EmptyString_ReturnsUnknown()
    {
        var text = "";

        var result = TextAnalyzer.GetReadingLevel(text);

        Assert.Equal("Unknown", result);
    }

    [Fact]
    public void GetReadingLevel_WhitespaceOnly_ReturnsUnknown()
    {
        var text = "   ";

        var result = TextAnalyzer.GetReadingLevel(text);

        Assert.Equal("Unknown", result);
    }

    [Fact]
    public void GetReadingLevel_SimpleShortText_ReturnsEasy()
    {
        var text = "I like cats. I like dogs.";

        var result = TextAnalyzer.GetReadingLevel(text);

        Assert.Equal("Easy", result);
    }

    [Fact]
    public void GetReadingLevel_ComplexLongWords_ReturnsHard()
    {
        var text = "The philosophical underpinnings of contemporary epistemological frameworks necessitate comprehensive examination. This fundamentally transforms our understanding of complexity.";

        var result = TextAnalyzer.GetReadingLevel(text);

        Assert.Equal("Hard", result);
    }

    [Fact]
    public void GetReadingLevel_LongSentences_ReturnsHard()
    {
        var text = "This sentence is quite long and continues with many words in sequence to ensure that the average sentence length exceeds the threshold value set for hard reading level determination.";

        var result = TextAnalyzer.GetReadingLevel(text);

        Assert.Equal("Hard", result);
    }

    [Fact]
    public void GetReadingLevel_MixedComplexityText_ReturnsMedium()
    {
        var text = "The quick brown fox jumps over the lazy dog. This is a test sentence.";

        var result = TextAnalyzer.GetReadingLevel(text);

        Assert.Equal("Medium", result);
    }

    [Fact]
    public void GetReadingLevel_NoSentenceDelimiters_ReturnsMedium()
    {
        var text = "The quick brown fox jumps over lazy dog animals";

        var result = TextAnalyzer.GetReadingLevel(text);

        Assert.Equal("Medium", result);
    }

    #endregion

    #region ExtractKeywords Tests

    [Fact]
    public void ExtractKeywords_NullText_ReturnsEmptyList()
    {
        string? text = null;
        var topN = 5;

        var result = TextAnalyzer.ExtractKeywords(text!, topN);

        Assert.Empty(result);
    }

    [Fact]
    public void ExtractKeywords_EmptyString_ReturnsEmptyList()
    {
        var text = "";
        var topN = 5;

        var result = TextAnalyzer.ExtractKeywords(text, topN);

        Assert.Empty(result);
    }

    [Fact]
    public void ExtractKeywords_WhitespaceOnly_ReturnsEmptyList()
    {
        var text = "   ";
        var topN = 5;

        var result = TextAnalyzer.ExtractKeywords(text, topN);

        Assert.Empty(result);
    }

    [Fact]
    public void ExtractKeywords_TopNLessThanOne_ReturnsEmptyList()
    {
        var text = "The quick brown fox";
        var topN = 0;

        var result = TextAnalyzer.ExtractKeywords(text, topN);

        Assert.Empty(result);
    }

    [Fact]
    public void ExtractKeywords_TopNNegative_ReturnsEmptyList()
    {
        var text = "The quick brown fox";
        var topN = -1;

        var result = TextAnalyzer.ExtractKeywords(text, topN);

        Assert.Empty(result);
    }

    [Fact]
    public void ExtractKeywords_FiltersStopWords_ReturnsOnlyContentWords()
    {
        var text = "the quick brown fox jumps";
        var topN = 5;

        var result = TextAnalyzer.ExtractKeywords(text, topN);

        Assert.DoesNotContain("the", result);
        Assert.Contains("quick", result);
        Assert.Contains("brown", result);
        Assert.Contains("fox", result);
        Assert.Contains("jumps", result);
    }

    [Fact]
    public void ExtractKeywords_ReturnsTopNMostFrequent_ReturnsCorrectCount()
    {
        var text = "apple apple banana banana banana cherry";
        var topN = 2;

        var result = TextAnalyzer.ExtractKeywords(text, topN);

        Assert.Equal(2, result.Count);
        Assert.Contains("banana", result);
        Assert.Contains("apple", result);
    }

    [Fact]
    public void ExtractKeywords_CaseInsensitive_CorrectlyAggregatesKeywords()
    {
        var text = "Apple APPLE apple Banana banana";
        var topN = 2;

        var result = TextAnalyzer.ExtractKeywords(text, topN);

        Assert.Equal(2, result.Count);
        Assert.Contains("apple", result);
        Assert.Contains("banana", result);
    }

    [Fact]
    public void ExtractKeywords_MoreKeywordsThanTopN_ReturnsExactlyTopN()
    {
        var text = "dog dog cat cat cat bird bird bird bird fish fish fish fish fish";
        var topN = 3;

        var result = TextAnalyzer.ExtractKeywords(text, topN);

        Assert.Equal(3, result.Count);
        Assert.Equal("fish", result[0]);
        Assert.Equal("bird", result[1]);
        Assert.Equal("cat", result[2]);
    }

    [Fact]
    public void ExtractKeywords_AllStopWords_ReturnsEmptyList()
    {
        var text = "the a an is in on at to and or but of for it this that";
        var topN = 5;

        var result = TextAnalyzer.ExtractKeywords(text, topN);

        Assert.Empty(result);
    }

    #endregion

    #region SummarizeStats Tests

    [Fact]
    public void SummarizeStats_NullText_ReturnsZeroStats()
    {
        string? text = null;

        var result = TextAnalyzer.SummarizeStats(text!);

        Assert.Equal(0, result.WordCount);
        Assert.Equal(0, result.SentenceCount);
        Assert.Equal(0, result.ParagraphCount);
        Assert.Equal(string.Empty, result.LongestWord);
        Assert.Equal(0.0, result.AverageWordLength);
    }

    [Fact]
    public void SummarizeStats_EmptyString_ReturnsZeroStats()
    {
        var text = "";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal(0, result.WordCount);
        Assert.Equal(0, result.SentenceCount);
        Assert.Equal(0, result.ParagraphCount);
        Assert.Equal(string.Empty, result.LongestWord);
        Assert.Equal(0.0, result.AverageWordLength);
    }

    [Fact]
    public void SummarizeStats_WhitespaceOnly_ReturnsZeroStats()
    {
        var text = "   ";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal(0, result.WordCount);
        Assert.Equal(0, result.SentenceCount);
        Assert.Equal(0, result.ParagraphCount);
        Assert.Equal(string.Empty, result.LongestWord);
        Assert.Equal(0.0, result.AverageWordLength);
    }

    [Fact]
    public void SummarizeStats_SingleWord_ReturnsCorrectStats()
    {
        var text = "Hello";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal(1, result.WordCount);
        Assert.Equal(0, result.SentenceCount);
        Assert.Equal(1, result.ParagraphCount);
        Assert.Equal("Hello", result.LongestWord);
        Assert.Equal(5.0, result.AverageWordLength);
    }

    [Fact]
    public void SummarizeStats_SimpleSentence_ReturnsCorrectStats()
    {
        var text = "The cat sat.";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal(3, result.WordCount);
        Assert.Equal(1, result.SentenceCount);
        Assert.Equal(1, result.ParagraphCount);
        Assert.Equal("cat", result.LongestWord);
        Assert.Equal(3.0, result.AverageWordLength);
    }

    [Fact]
    public void SummarizeStats_MultipleSentences_CountsSentencesCorrectly()
    {
        var text = "Hello world. This is great! Are you ready?";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal(6, result.WordCount);
        Assert.Equal(3, result.SentenceCount);
        Assert.Equal(1, result.ParagraphCount);
    }

    [Fact]
    public void SummarizeStats_MultipleParagraphs_CountsParagraphsCorrectly()
    {
        var text = "First paragraph with text.\n\nSecond paragraph here.";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal(2, result.ParagraphCount);
    }

    [Fact]
    public void SummarizeStats_MultipleParagraphsWithCarriageReturn_CountsParagraphsCorrectly()
    {
        var text = "First paragraph.\r\n\r\nSecond paragraph.";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal(2, result.ParagraphCount);
    }

    [Fact]
    public void SummarizeStats_IdentifiesLongestWord_ReturnsCorrectWord()
    {
        var text = "The extraordinary phenomenon appeared.";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal("extraordinary", result.LongestWord);
    }

    [Fact]
    public void SummarizeStats_CalculatesAverageWordLength_ReturnsRoundedValue()
    {
        var text = "dog cat bird fish";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal(4, result.WordCount);
        Assert.Equal(3.8, result.AverageWordLength);
    }

    [Fact]
    public void SummarizeStats_ComplexText_ReturnsCompleteStats()
    {
        var text = "The quick brown fox jumps over the lazy dog. It was amazing.";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal(11, result.WordCount);
        Assert.Equal(2, result.SentenceCount);
        Assert.Equal(1, result.ParagraphCount);
        Assert.Equal("amazing", result.LongestWord);
        Assert.True(result.AverageWordLength > 0);
    }

    [Fact]
    public void SummarizeStats_TextWithPunctuation_CountsWordsCorrectly()
    {
        var text = "Don't worry, it's fine!";

        var result = TextAnalyzer.SummarizeStats(text);

        Assert.Equal(4, result.WordCount);
    }

    #endregion

    #region ContainsRepeatedWords Tests

    [Fact]
    public void ContainsRepeatedWords_NullText_ReturnsFalse()
    {
        string? text = null;

        var result = TextAnalyzer.ContainsRepeatedWords(text!);

        Assert.False(result);
    }

    [Fact]
    public void ContainsRepeatedWords_EmptyString_ReturnsFalse()
    {
        var text = "";

        var result = TextAnalyzer.ContainsRepeatedWords(text);

        Assert.False(result);
    }

    [Fact]
    public void ContainsRepeatedWords_WhitespaceOnly_ReturnsFalse()
    {
        var text = "   ";

        var result = TextAnalyzer.ContainsRepeatedWords(text);

        Assert.False(result);
    }

    [Fact]
    public void ContainsRepeatedWords_NoRepeatedWords_ReturnsFalse()
    {
        var text = "The quick brown fox jumps over the lazy dog";

        var result = TextAnalyzer.ContainsRepeatedWords(text);

        Assert.False(result);
    }

    [Fact]
    public void ContainsRepeatedWords_AdjacentRepeatedWords_ReturnsTrue()
    {
        var text = "This this is a test";

        var result = TextAnalyzer.ContainsRepeatedWords(text);

        Assert.True(result);
    }

    [Fact]
    public void ContainsRepeatedWords_AdjacentRepeatedWordsDifferentCase_ReturnsTrue()
    {
        var text = "This THIS is repeated";

        var result = TextAnalyzer.ContainsRepeatedWords(text);

        Assert.True(result);
    }

    [Fact]
    public void ContainsRepeatedWords_RepeatedWordsNotAdjacent_ReturnsFalse()
    {
        var text = "The quick brown fox and the lazy dog";

        var result = TextAnalyzer.ContainsRepeatedWords(text);

        Assert.False(result);
    }

    [Fact]
    public void ContainsRepeatedWords_MultipleAdjacentRepetitions_ReturnsTrue()
    {
        var text = "Very very very good";

        var result = TextAnalyzer.ContainsRepeatedWords(text);

        Assert.True(result);
    }

    [Fact]
    public void ContainsRepeatedWords_CaseInsensitiveMatching_ReturnsTrue()
    {
        var text = "HELLO hello world";

        var result = TextAnalyzer.ContainsRepeatedWords(text);

        Assert.True(result);
    }

    [Fact]
    public void ContainsRepeatedWords_SingeWord_ReturnsFalse()
    {
        var text = "Hello";

        var result = TextAnalyzer.ContainsRepeatedWords(text);

        Assert.False(result);
    }

    #endregion
}
