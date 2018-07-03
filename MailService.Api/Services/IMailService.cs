namespace MailService.Api.Services
{
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using global::MailService.Api.Models;

    // the interface of mail service
    public interface IMailService
    {
        // the api verb to send email through the third party APIs
        string ApiUri { get; }

        // the authentication header required to connect to the third party APIs
        AuthenticationHeaderValue AuthenticationHeaderValue { get; }

        // the base url to connect to the third party APIs
        string MailServiceUri { get; }
        
        // function resposible for sending the mail by calling the http client
        Task<bool> Send(MailModel mailModel);
    }
}