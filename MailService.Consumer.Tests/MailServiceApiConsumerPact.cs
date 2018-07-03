namespace MailService.Consumer.Tests
{
    using System;

    using PactNet;
    using PactNet.Mocks.MockHttpService;

    public class MailServiceApiConsumerPact : IDisposable
    {
        public MailServiceApiConsumerPact()
        {
            PactBuilder =
                new PactBuilder(); // Defaults to specification version 1.1.0, uses default directories. PactDir: ..\..\pacts and LogDir: ..\..\logs

            PactBuilder.ServiceConsumer("Mail Service Consumer").HasPactWith("Mail Service API");

            MockProviderService = PactBuilder.MockService(MockServerPort); // Configure the http mock server
        }

        public IMockProviderService MockProviderService { get; private set; }

        public string MockProviderServiceBaseUri
        {
            get
            {
                return string.Format("http://localhost:{0}", MockServerPort);
            }
        }

        public int MockServerPort
        {
            get
            {
                return 8080;
            }
        }

        public IPactBuilder PactBuilder { get; private set; }

        public void Dispose()
        {
            PactBuilder.Build(); // NOTE: Will save the pact file once finished
        }
    }
}