using Creative.Controllers;
using Creative.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Creative.Tests.Controllers;

public class HomeControllerTests
{
    private readonly HomeController _controller;

    public HomeControllerTests()
    {
        var mockLogger = new Mock<ILogger<HomeController>>();
        _controller = new HomeController(mockLogger.Object);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }

    [Fact]
    public void Index_ReturnsViewResult()
    {
        var result = _controller.Index();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Privacy_ReturnsViewResult()
    {
        var result = _controller.Privacy();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Error_ReturnsViewResult()
    {
        var result = _controller.Error();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Error_ViewModelHasRequestId()
    {
        var result = (ViewResult)_controller.Error();
        var model = Assert.IsType<ErrorViewModel>(result.Model);

        Assert.NotNull(model.RequestId);
    }

    [Fact]
    public void Error_ViewModelRequestId_UsesTraceIdentifier_WhenNoActivity()
    {
        _controller.ControllerContext.HttpContext.TraceIdentifier = "test-trace-id";

        var result = (ViewResult)_controller.Error();
        var model = Assert.IsType<ErrorViewModel>(result.Model);

        Assert.Equal("test-trace-id", model.RequestId);
    }
}
