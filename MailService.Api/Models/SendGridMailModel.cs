namespace MailService.Api.Models
{
    using System.Globalization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    // this model is used to map the mailmodel to a model which is compatible with the send grid json content requirements
    public partial class SendGridMailModel
    {
        [JsonProperty("content")]
        public Content[] Content { get; set; }

        [JsonProperty("from")]
        public Contact From { get; set; }

        [JsonProperty("personalizations")]
        public Personalization[] Personalizations { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
    }

    public partial class Content
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Contact
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public partial class Personalization
    {
        [JsonProperty("bcc")]
        public Contact[] Bcc { get; set; }

        [JsonProperty("cc")]
        public Contact[] Cc { get; set; }

        [JsonProperty("to")]
        public Contact[] To { get; set; }
    }

    public partial class SendGridMailModel
    {
        public static SendGridMailModel FromJson(string json) =>
            JsonConvert.DeserializeObject<SendGridMailModel>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this SendGridMailModel self) =>
            JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings =
            new JsonSerializerSettings
                {
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    Converters =
                        {
                            new IsoDateTimeConverter
                                {
                                    DateTimeStyles =
                                        DateTimeStyles
                                            .AssumeUniversal
                                }
                        },
                };
    }
}