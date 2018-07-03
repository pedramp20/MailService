namespace MailService.Api.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using global::MailService.Api.Models;

    // the abstract class holding the shared functionality of all derived classes
    public abstract class MailService : IMailService
    {
        public abstract string ApiUri { get; }

        public abstract AuthenticationHeaderValue AuthenticationHeaderValue { get; }

        public abstract string MailServiceUri { get; }

        public async Task<bool> Send(MailModel mailModel)
        {
            using (var client = new HttpClient { BaseAddress = new Uri(MailServiceUri) })
            {
                var content = await ToHttpContent(mailModel);
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue;
                HttpStatusCode status;
                try
                {
                    using (var response = await client.PostAsync(ApiUri, content))
                    {
                        status = response.StatusCode;

                        // if the sending is unsuccessful for any reason an exception is thrown,
                        // since the fall back mechanism relies on catching exceptions
                        if (!response.IsSuccessStatusCode)
                            throw new Exception($"Unsuccessful to send email- status: {status}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was a problem connecting to the mail service API.", ex);
                }

                return status == HttpStatusCode.Accepted || status == HttpStatusCode.OK;
            }
        }

        protected abstract Task<HttpContent> ToHttpContent(MailModel mailModel);
    }
}