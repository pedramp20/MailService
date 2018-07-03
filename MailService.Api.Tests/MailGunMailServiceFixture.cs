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
                () => new MailSecrets { MailGunServiceApiKey = "0d0abac39005b10f88e1f4fe0f72a054-e44cc7c1-3a8b452c" });

            MailGunMailService = new MailGunMailService(optionsMock.Object);
        }

        public MailGunMailService MailGunMailService { get; set; }

        public void Dispose()
        {
        }
    }
}