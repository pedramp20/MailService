using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Api.Tests
{
    using FluentAssertions;

    using MailService.Api.Models;

    using Xunit;

    public class MapperTests : IClassFixture<MapperFixture>
    {
        private readonly MapperFixture mapperFixture;

        public MapperTests(MapperFixture mapperFixture)
        {
            this.mapperFixture = mapperFixture;
        }

        [Fact]
        public void MapToSendGridMailModel_WhenCompleteMailModelIsPassed_ReturnsEquivalentSendGridMailModel()
        {
            // Arrange 
            var mailModel = new MailModel
                                {
                                    To = new List<string>() { "pedram.pourashraf@gmail.com" },
                                    Cc = new List<string>() { "pedram1@alfabit.com.au" },
                                    Bcc = new List<string>() { "pedram2@alfabit.com.au" },
                                    From = "pedram@mailservice.com",
                                    Subject = "Unit test was successful",
                                    Body = "This is a message sent from unit test.",
                                };
            var expectedModel = new SendGridMailModel
                                    {
                                        Personalizations =
                                            new[]
                                                {
                                                    new Personalization
                                                        {
                                                            To =
                                                                new[]
                                                                    {
                                                                        new
                                                                        Contact
                                                                            {
                                                                                Email
                                                                                    = "pedram.pourashraf@gmail.com"
                                                                            }
                                                                    },
                                                            Cc = new[]
                                                                     {
                                                                         new
                                                                         Contact
                                                                             {
                                                                                 Email
                                                                                     = "pedram1@alfabit.com.au"
                                                                             }
                                                                     },
                                                            Bcc = new[]
                                                                      {
                                                                          new
                                                                          Contact
                                                                              {
                                                                                  Email
                                                                                      = "pedram2@alfabit.com.au"
                                                                              }
                                                                      },
                                                        }
                                                },
                                        From = new Contact { Email = "pedram@mailservice.com" },
                                        Subject = "Unit test was successful",
                                        Content = new[]
                                                      {
                                                          new Content
                                                              {
                                                                  Type = "text/plain",
                                                                  Value =
                                                                      "This is a message sent from unit test."
                                                              }
                                                      }
                                    };
            // Act
            var result = mapperFixture.Mapper.Map<SendGridMailModel>(mailModel);
            // Assert
            result.Should().BeEquivalentTo(expectedModel);
        }
        [Fact]
        public void MapToSendGridMailModel_WhenIncompleteMailModelIsPassed_ReturnsEquivalentSendGridMailModel()
        {
            // Arrange 
            var mailModel = new MailModel
                                {
                                    To = new List<string>() { "pedram.pourashraf@gmail.com" },
                                    Cc = new List<string>() { "pedram1@alfabit.com.au" },
                                    From = "pedram@mailservice.com",
                                    Subject = "Unit test was successful",
                                    Body = "This is a message sent from unit test.",
                                };
            var expectedModel = new SendGridMailModel
                                    {
                                        Personalizations =
                                            new[]
                                                {
                                                    new Personalization
                                                        {
                                                            To =
                                                                new[]
                                                                    {
                                                                        new
                                                                        Contact
                                                                            {
                                                                                Email
                                                                                    = "pedram.pourashraf@gmail.com"
                                                                            }
                                                                    },
                                                            Cc = new[]
                                                                     {
                                                                         new
                                                                         Contact
                                                                             {
                                                                                 Email
                                                                                     = "pedram1@alfabit.com.au"
                                                                             }
                                                                     }
                                                        }
                                                },
                                        From = new Contact { Email = "pedram@mailservice.com" },
                                        Subject = "Unit test was successful",
                                        Content = new[]
                                                      {
                                                          new Content
                                                              {
                                                                  Type = "text/plain",
                                                                  Value =
                                                                      "This is a message sent from unit test."
                                                              }
                                                      }
                                    };
            // Act
            var result = mapperFixture.Mapper.Map<SendGridMailModel>(mailModel);
            // Assert
            result.Should().BeEquivalentTo(expectedModel);
        }
    }
}
