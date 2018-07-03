namespace MailService.Api.Models
{
    // to retrieve the Api key from the config file
    public class MailSecrets
    {
        // MailGun Api key
        public string MailGunServiceApiKey { get; set; }

        // SendGrid Api key
        public string SendGridServiceApiKey { get; set; }
    }
}