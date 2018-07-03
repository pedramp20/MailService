namespace MailService.Consumer.Services
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using global::MailService.Consumer.Models;

    using Newtonsoft.Json;

    public class MailService : IMailService
    {
        private const string RemoteMailApiUri = "api/mail";

        public MailService(string baseUri = null)
        {
            BaseUri = baseUri ?? "http://localhost:8080/";
        }

        public string BaseUri { get; set; }

        public async Task<MailResponseModel> Send(MailModel mail)
        {
            string reasonPhrase;

            using (var client = new HttpClient { BaseAddress = new Uri(BaseUri) })
            {
                var json = JsonConvert.SerializeObject(mail);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                HttpStatusCode status;
                string content;
                try
                {
                    using (var response = await client.PostAsync(RemoteMailApiUri, stringContent))
                    {
                        status = response.StatusCode;
                        content = response.Content.ReadAsStringAsync().Result;

                        reasonPhrase = response.ReasonPhrase;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was a problem connecting to the mail service API.", ex);
                }

                if (status == HttpStatusCode.OK)
                {
                    return !string.IsNullOrEmpty(content)
                               ? JsonConvert.DeserializeObject<MailResponseModel>(content)
                               : null;
                }
            }

            throw new Exception(reasonPhrase);
        }
    }
}