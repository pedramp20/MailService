namespace MailService.Consumer.Tests
{
    using System.Threading.Tasks;

    using FluentAssertions;

    using MailService.Consumer.Controllers;
    using MailService.Consumer.Models;
    using MailService.Consumer.Services;

    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using Xunit;

    public class MailControllerTests
    {
        [Fact]
        public async Task IndexPost_WhenModelStateIsInvalid_ReturnsBadRequestResult()
        {
            // Arrange
            var mockMailService = new Mock<IMailService>();
            var mailController = new MailController(mockMailService.Object);
            mailController.ModelState.AddModelError("From", "Required");
            var mailModel = new MailModel();

            // Act
            var result = await mailController.Index(mailModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<SerializableError>();
        }

        [Fact]
        public async Task IndexPost_WhenModelStateIsValid_ReturnsMailResponseModel()
        {
            // Arrange 
            var mockMailService = new Mock<IMailService>();
            mockMailService.Setup(m => m.Send(It.IsAny<MailModel>())).ReturnsAsync(() => new MailResponseModel());
            var mailController = new MailController(mockMailService.Object);
            var mailModel = new MailModel();

            // Act
            var result = await mailController.Index(mailModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<MailResponseModel>();
        }
    }
}