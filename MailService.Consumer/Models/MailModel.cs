namespace MailService.Consumer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MailModel
    {
        // Email address of the bcced recipient(s). 
        public IEnumerable<string> Bcc { get; set; }

        // Body of the message
        [Required]
        public string Body { get; set; }

        // Email address of the cced recipient(s). 
        public IEnumerable<string> Cc { get; set; }

        // Email address for From header
        [Required]
        public string From { get; set; }

        // Subject of the message
        [Required]
        public string Subject { get; set; }

        // Email address of the recipient(s). 
        [Required]
        public IEnumerable<string> To { get; set; }
    }
}