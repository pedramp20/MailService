namespace MailService.Consumer.Models
{
    public class MailResponseModel
    {
        // returns the success of retieving data from backend
        public bool Success { get; set; }
        // retrieve the message sent from backend
        public string Message { get; set; }
    }
}