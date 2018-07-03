# MAil Service

Mail service is a simple email service that accepts the necessary information and sends emails. The front end of the service is implemented using Asp .Net Core + Angular and the back end is developed using Asp .Net Core Web API. A third-party email service API provider known as MailGun is integrated into the back-end to send it deliver emails to the requested recipients. The service also utilises a simple failover mechanism to gracefully fail over to another third-party service (Send Grid) when the primary service provider is unavailable.


### Installing

Install [.NET Core 2.1 or later](https://www.microsoft.com/net/download/windows)

Install you favourite code editor such [VS Code](https://code.visualstudio.com/) or IDE [Visual Studio 2017](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio)

### Prerequisites

Register your acounts on [Mail Gun](https://www.mailgun.com/) and [Send Grid](https://sendgrid.com/) and update the respective values in appsettings.json file in MailService.Api project. 

```
{
  "Logging": {
    "IncludeScopes": false,
    "Debug": {
      "LogLevel": {
        "Default": "Warning"
      }
    },
    "Console": {
      "LogLevel": {
        "Default": "Warning"
      }
    }
  },
  "MailServicesSecrets": {
    "MailGunServiceApiKey": "Enter Your API Key HERE",
    "SendGridServiceApiKey": "Enter Your API Key HERE"
  }
}

```



Finally update the values in the test fixture classes under the MailService.Api.Tests project.

```
 public SendGridMailServiceFixture()
        {
            var optionsMock = new Mock<IOptions<MailSecrets>>();
            optionsMock.Setup(o => o.Value).Returns(
                () => new MailSecrets
                          {
                              SendGridServiceApiKey =
                                  ">>Update Your Sed Grid Api Key<<"
                          });

            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new DomainProfile()); });
            var mapper = config.CreateMapper();

            SendGridMailService = new SendGridMailService(optionsMock.Object, mapper);
        }
```

And update your api key value in MailGunMailServiceFixture

```
 public MailGunMailServiceFixture()
        {
            var optionsMock = new Mock<IOptions<MailSecrets>>();
            optionsMock.Setup(o => o.Value).Returns(
                () => new MailSecrets { MailGunServiceApiKey = ">>Update your mail gun Api key<<" });

            MailGunMailService = new MailGunMailService(optionsMock.Object);
        }
```

## Running the projects

Right click on the solution and select properties. Under Startup project select "multiple setup projects" and from the list choose MailService.Api and MailService.Consumer and press ok.

### Try the project

Run the projects and give it a try. You should be able to see a single page web app designed to compose and send emails.

## Authors

* **Pedram Pourashraf**

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* AutoMapper
* FluentAssertions
* Moq
* PactNet
* XUnit

