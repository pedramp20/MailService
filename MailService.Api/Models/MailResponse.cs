namespace MailService.Api.Models
{
    public class MailResponse
    {
        // the message sent to the front end and eventually being shown to the end user
        public string Message { get; set; }

        // true returned when the status code of the http response from the third party api is SuccessStatusCode
        public bool Success { get; set; }
    }
}