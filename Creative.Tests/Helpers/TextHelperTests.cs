using Creative.Helpers;
using Xunit;

namespace Creative.Tests.Helpers;

public class TextHelperTests
{
    #region Truncate Tests

    [Fact]
    public void Truncate_ValidInput_ReturnsExpectedResult()
    {
        var text = "The quick brown fox jumps over the lazy dog";
        var maxLength = 10;

        var result = TextHelper.Truncate(text, maxLength);

        Assert.Equal("The quick ...", result);
    }

    [Fact]
    public void Truncate_TextExactlyAtMaxLength_ReturnsOriginalText()
    {
        var text = "Hello";
        var maxLength = 5;

        var result = TextHelper.Truncate(text, maxLength);

        Assert.Equal("Hello", result);
    }

    [Fact]
    public void Truncate_TextShorterThanMaxLength_ReturnsOriginalText()
    {
        var text = "Hi";
        var maxLength = 10;

        var result = TextHelper.Truncate(text, maxLength);

        Assert.Equal("Hi", result);
    }

    [Fact]
    public void Truncate_EmptyString_ReturnsEmptyString()
    {
        var text = "";
        var maxLength = 5;

        var result = TextHelper.Truncate(text, maxLength);

        Assert.Equal("", result);
    }

    [Fact]
    public void Truncate_NullString_ReturnsNull()
    {
        string text = null;
        var maxLength = 5;

        var result = TextHelper.Truncate(text, maxLength);

        Assert.Null(result);
    }

    #endregion

    #region WordCount Tests

    [Fact]
    public void WordCount_ValidInput_ReturnsExpectedResult()
    {
        var text = "The quick brown fox";

        var result = TextHelper.WordCount(text);

        Assert.Equal(4, result);
    }

    [Fact]
    public void WordCount_SingleWord_ReturnsOne()
    {
        var text = "Hello";

        var result = TextHelper.WordCount(text);

        Assert.Equal(1, result);
    }

    [Fact]
    public void WordCount_MultipleSpaces_IgnoresExtraSpaces()
    {
        var text = "Hello    world    test";

        var result = TextHelper.WordCount(text);

        Assert.Equal(3, result);
    }

    [Fact]
    public void WordCount_TextWithLeadingAndTrailingSpaces_CountsCorrectly()
    {
        var text = "  The quick brown fox  ";

        var result = TextHelper.WordCount(text);

        Assert.Equal(4, result);
    }

    [Fact]
    public void WordCount_EmptyString_ReturnsZero()
    {
        var text = "";

        var result = TextHelper.WordCount(text);

        Assert.Equal(0, result);
    }

    [Fact]
    public void WordCount_WhitespaceOnly_ReturnsZero()
    {
        var text = "   ";

        var result = TextHelper.WordCount(text);

        Assert.Equal(0, result);
    }

    [Fact]
    public void WordCount_NullString_ReturnsZero()
    {
        string text = null;

        var result = TextHelper.WordCount(text);

        Assert.Equal(0, result);
    }

    #endregion

    #region Capitalize Tests

    [Fact]
    public void Capitalize_ValidInput_ReturnsExpectedResult()
    {
        var text = "hello world";

        var result = TextHelper.Capitalize(text);

        Assert.Equal("Hello world", result);
    }

    [Fact]
    public void Capitalize_AllLowercase_CapitalizesFirst()
    {
        var text = "test";

        var result = TextHelper.Capitalize(text);

        Assert.Equal("Test", result);
    }

    [Fact]
    public void Capitalize_AlreadyCapitalized_RemainCapitalizedAndLowerRest()
    {
        var text = "HELLO";

        var result = TextHelper.Capitalize(text);

        Assert.Equal("Hello", result);
    }

    [Fact]
    public void Capitalize_SingleCharacter_CapitalizesCharacter()
    {
        var text = "a";

        var result = TextHelper.Capitalize(text);

        Assert.Equal("A", result);
    }

    [Fact]
    public void Capitalize_EmptyString_ReturnsEmptyString()
    {
        var text = "";

        var result = TextHelper.Capitalize(text);

        Assert.Equal("", result);
    }

    [Fact]
    public void Capitalize_NullString_ReturnsNull()
    {
        string text = null;

        var result = TextHelper.Capitalize(text);

        Assert.Null(result);
    }

    #endregion

    #region Slugify Tests

    [Fact]
    public void Slugify_ValidInput_ReturnsExpectedResult()
    {
        var text = "Hello World Example";

        var result = TextHelper.Slugify(text);

        Assert.Equal("hello-world-example", result);
    }

    [Fact]
    public void Slugify_MixedCase_ConvertsToLowercase()
    {
        var text = "MyTestString";

        var result = TextHelper.Slugify(text);

        Assert.Equal("myteststring", result);
    }

    [Fact]
    public void Slugify_MultipleConsecutiveSpaces_CollapsesDashes()
    {
        var text = "Hello  world   test";

        var result = TextHelper.Slugify(text);

        Assert.Equal("hello-world--test", result);
    }

    [Fact]
    public void Slugify_LeadingAndTrailingSpaces_TrimsThenSlugifies()
    {
        var text = "  Hello World  ";

        var result = TextHelper.Slugify(text);

        Assert.Equal("hello-world", result);
    }

    [Fact]
    public void Slugify_SingleWord_ReturnsLowercase()
    {
        var text = "Test";

        var result = TextHelper.Slugify(text);

        Assert.Equal("test", result);
    }

    [Fact]
    public void Slugify_EmptyString_ReturnsEmptyString()
    {
        var text = "";

        var result = TextHelper.Slugify(text);

        Assert.Equal("", result);
    }

    [Fact]
    public void Slugify_WhitespaceOnly_ReturnsEmptyString()
    {
        var text = "   ";

        var result = TextHelper.Slugify(text);

        Assert.Equal("", result);
    }

    [Fact]
    public void Slugify_NullString_ReturnsEmptyString()
    {
        string text = null;

        var result = TextHelper.Slugify(text);

        Assert.Equal("", result);
    }

    #endregion
}
