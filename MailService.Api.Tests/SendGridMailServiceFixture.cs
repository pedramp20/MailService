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
                                  ">>Update your API key<<"
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