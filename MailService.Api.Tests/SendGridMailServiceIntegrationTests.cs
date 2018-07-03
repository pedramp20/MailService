namespace MailService.Api.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FluentAssertions;

    using MailService.Api.Models;

    using Xunit;

    public class SendGridMailServiceIntegrationTests : IClassFixture<SendGridMailServiceFixture>
    {
        private readonly SendGridMailServiceFixture sendGridMailServiceFixture;

        public SendGridMailServiceIntegrationTests(SendGridMailServiceFixture sendGridMailServiceFixture)
        {
            this.sendGridMailServiceFixture = sendGridMailServiceFixture;
        }

        [Fact]
        public async Task Send_WhenCalled_ReturnsTrue()
        {
            // Arrange 
            var mailModel = new MailModel
                                {
                                    To = new List<string>() { "pedram.pourashraf@gmail.com" },
                                    Cc = new List<string>() { "pedram@alfabit.com.au" },
                                    From = "pedram@mailservice.com",
                                    Subject = "Integration test was successful",
                                    Body = "This is a message sent from integration test.",
                                };

            // Act
            var result = await sendGridMailServiceFixture.SendGridMailService.Send(mailModel);

            // Assert
            result.Should().BeTrue();
        }
    }
}