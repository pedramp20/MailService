namespace MailService.Api.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FluentAssertions;

    using MailService.Api.Models;

    using Xunit;

    public class MailGunMailServiceIntegrationTests : IClassFixture<MailGunMailServiceFixture>
    {
        private readonly MailGunMailServiceFixture mailGunMailServiceFixture;

        public MailGunMailServiceIntegrationTests(MailGunMailServiceFixture mailGunMailServiceFixture)
        {
            this.mailGunMailServiceFixture = mailGunMailServiceFixture;
        }

        [Fact]
        public async Task Send_WhenCalled_ReturnsTrue()
        {
            // Arrange 
            var mailModel = new MailModel
                                {
                                    To = new List<string>() { "pedram.pourashraf@gmail.com" },
                                    //Cc = new List<string>() { "pedram@alfabit.com.au" }, // Note: cc is not supported with the test domain
                                    From = "pedram@mailservice.com",
                                    Subject = "Integration test was successful",
                                    Body = "This is a message sent from integration test.",
                                };

            // Act
            var result  = await mailGunMailServiceFixture.MailGunMailService.Send(mailModel);
              
            // Assert
            result.Should().BeTrue();
        }
    }
}