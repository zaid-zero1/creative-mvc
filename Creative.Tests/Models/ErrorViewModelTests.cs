using Creative.Models;
using Xunit;

namespace Creative.Tests.Models;

public class ErrorViewModelTests
{
    [Fact]
    public void ShowRequestId_IsTrue_WhenRequestIdIsSet()
    {
        var model = new ErrorViewModel { RequestId = "abc-123" };

        Assert.True(model.ShowRequestId);
    }

    [Fact]
    public void ShowRequestId_IsFalse_WhenRequestIdIsNull()
    {
        var model = new ErrorViewModel { RequestId = null };

        Assert.False(model.ShowRequestId);
    }

    [Fact]
    public void ShowRequestId_IsFalse_WhenRequestIdIsEmpty()
    {
        var model = new ErrorViewModel { RequestId = string.Empty };

        Assert.False(model.ShowRequestId);
    }
}
