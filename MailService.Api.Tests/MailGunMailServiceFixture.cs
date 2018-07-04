namespace MailService.Api.Tests
{
    using System;

    using MailService.Api.Models;
    using MailService.Api.Services;

    using Microsoft.Extensions.Options;

    using Moq;

    public class MailGunMailServiceFixture : IDisposable
    {
        public MailGunMailServiceFixture()
        {
            var optionsMock = new Mock<IOptions<MailSecrets>>();
            optionsMock.Setup(o => o.Value).Returns(
                () => new MailSecrets { MailGunServiceApiKey = ">>Update your API key<<" });

            MailGunMailService = new MailGunMailService(optionsMock.Object);
        }

        public MailGunMailService MailGunMailService { get; set; }

        public void Dispose()
        {
        }
    }
}