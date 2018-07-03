namespace MailService.Api.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;

    using global::MailService.Api.Models;

    using Microsoft.Extensions.Options;

    public class SendGridMailService : MailService
    {
        // holds the required API key
        private readonly IOptions<MailSecrets> mailSecrets;

        // to map the domain model to resource model
        private readonly IMapper mapper;

        public SendGridMailService(IOptions<MailSecrets> mailSecrets, IMapper mapper)
        {
            this.mailSecrets = mailSecrets;
            this.mapper = mapper;
        }

        public override string ApiUri => "mail/send";

        // required authentication header to access sendgrid service
        public override AuthenticationHeaderValue AuthenticationHeaderValue =>
            new AuthenticationHeaderValue("Bearer", mailSecrets.Value.SendGridServiceApiKey);

        public override string MailServiceUri => "https://api.sendgrid.com/v3/";

        // converts the generic mail model to json and finally returns a http content
        protected override async Task<HttpContent> ToHttpContent(MailModel mailModel)
        {
            try
            {
                var sendGridMailModel = mapper.Map<SendGridMailModel>(mailModel);
                return new StringContent(sendGridMailModel.ToJson(), Encoding.UTF8, "application/json");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}