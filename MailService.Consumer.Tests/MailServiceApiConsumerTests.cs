namespace MailService.Consumer.Tests
{
    using System.Collections.Generic;

    using MailService.Consumer.Models;
    using MailService.Consumer.Services;

    using PactNet.Mocks.MockHttpService;
    using PactNet.Mocks.MockHttpService.Models;

    using Xunit;

    public class MailServiceApiConsumerTests : IClassFixture<MailServiceApiConsumerPact>
    {
        private readonly IMockProviderService mockProviderService;

        private readonly string mockProviderServiceBaseUri;

        public MailServiceApiConsumerTests(MailServiceApiConsumerPact data)
        {
            mockProviderService = data.MockProviderService;
            mockProviderService
                .ClearInteractions(); // NOTE: Clears any previously registered interactions before the test is run
            mockProviderServiceBaseUri = data.MockProviderServiceBaseUri;
        }

        [Fact]
        public void Send_WhenTheRightMailModelIsPassed_ReturnsARightMailModelResponse()
        {
            // Arrange
            mockProviderService.Given("A valid mail model containing all required elements")
                .UponReceiving("A POST request to send the mail")
                .With(
                    new ProviderServiceRequest
                        {
                            Method = HttpVerb.Post,
                            Path = "/api/mail",
                            Headers = new Dictionary<string, object>
                                          {
                                              {
                                                  "Content-Type",
                                                  "application/json; charset=utf-8"
                                              }
                                          }
                        }).WillRespondWith(
                    new ProviderServiceResponse
                        {
                            Status = 200,
                            Headers =
                                new Dictionary<string, object>
                                    {
                                        {
                                            "Content-Type",
                                            "application/json; charset=utf-8"
                                        }
                                    },
                            Body = new { message = "The mail has been sent successfully." }

                            // NOTE: Note the case sensitivity here, the body will be serialised as per the casing defined
                        }); // NOTE: WillRespondWith call must come last as it will register the interaction

            var consumer = new MailService(mockProviderServiceBaseUri);
            var mail = new MailModel
                           {
                               From = "me@me.com",
                               To = new List<string>() { "you@you.com" },
                               Cc = new List<string>() { "her@her.com" },
                               Bcc = new List<string>() { "him@him.com" },
                               Subject = "greeting",
                               Body = "hello!"
                           };

            // Act
            var response = consumer.Send(mail);

            // Assert
            Assert.Equal("The mail has been sent successfully.", response.Result.Message);

            mockProviderService
                .VerifyInteractions(); // NOTE: Verifies that interactions registered on the mock provider are called once and only once
        }
    }
}