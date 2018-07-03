namespace MailService.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using global::MailService.Api.Models;

    using Microsoft.Extensions.Options;

    public class MailGunMailService : MailService
    {
        // the domain name of mail gun service NOTE: replace it with the actual domain name before publishing
        private const string Domain = "sandboxbba7109780a44be3861042f6e511b166.mailgun.org/";

        // holds the required API key
        private readonly IOptions<MailSecrets> mailSecrets;

        public MailGunMailService(IOptions<MailSecrets> mailSecrets)
        {
            this.mailSecrets = mailSecrets;
        }

        public override string ApiUri => "messages";

        // basic authetication is required for accessing mail gun service
        public override AuthenticationHeaderValue AuthenticationHeaderValue =>
            new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "api", mailSecrets.Value.MailGunServiceApiKey))));

        public override string MailServiceUri => "https://api.mailgun.net/v3/" + Domain;

        // converts to the supported format and returns a http content from the generic mail model
        protected override async Task<HttpContent> ToHttpContent(MailModel mailModel)
        {
            var form = new Dictionary<string, string>
                           {
                               ["from"] = mailModel.From,
                               ["to"] = string.Join(',', mailModel.To),
                               ["subject"] = mailModel.Subject,
                               ["text"] = mailModel.Body
                           };

            if (mailModel.Cc != null && mailModel.Cc.Any())
            {
                form.Add("cc", string.Join(',', mailModel.Cc));
            }

            if (mailModel.Bcc != null && mailModel.Bcc.Any())
            {
                form.Add("bcc", string.Join(',', mailModel.Bcc));
            }

            return new FormUrlEncodedContent(form);
        }
    }
}