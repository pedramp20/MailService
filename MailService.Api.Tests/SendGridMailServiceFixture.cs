namespace MailService.Api.Tests
{
    using System;

    using AutoMapper;

    using MailService.Api.Models;
    using MailService.Api.Profiles;
    using MailService.Api.Services;

    using Microsoft.Extensions.Options;

    using Moq;

    public class SendGridMailServiceFixture : IDisposable
    {
        public SendGridMailServiceFixture()
        {
            var optionsMock = new Mock<IOptions<MailSecrets>>();
            optionsMock.Setup(o => o.Value).Returns(
                () => new MailSecrets
                          {
                              SendGridServiceApiKey =
                                  "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"
                          });

            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new DomainProfile()); });
            var mapper = config.CreateMapper();

            SendGridMailService = new SendGridMailService(optionsMock.Object, mapper);
        }

        public SendGridMailService SendGridMailService { get; set; }

        public void Dispose()
        {
        }
    }
}