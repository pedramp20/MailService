namespace MailService.Api.Tests
{
    using System.Threading.Tasks;

    using FluentAssertions;

    using MailService.Api.Controllers;

    using Microsoft.AspNetCore.Mvc;

    using Xunit;

    public class MailControllerTests
    {
        [Fact]
        public async Task Index_WhenCalled_ReturnsAContentResultWithAWelcomeMessage()
        {
            // Arrange
            var controller = new MailController(null);

            // Act
            var result = await controller.Index();

            // Assert
            result.Should().BeOfType<ContentResult>().Which.Content.Should().Be("Welcome to the backend mail API");
        }
    }
}