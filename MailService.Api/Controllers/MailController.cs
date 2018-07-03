namespace MailService.Api.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using MailService.Api.Models;
    using MailService.Api.Services;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [Route("api/[controller]")]
    public class MailController : Controller
    {
        private readonly MailGunMailService mailGunMailService;

        private readonly SendGridMailService sendGridMailService;

        public MailController(IServiceProvider serviceProvider)
        {
            // to satisfy cases for initial unit tests
            if (serviceProvider == null)
                return;

            // since two implementation of the IMailService interface are injected in the startup, GetServices is used to resolved both of them
            var services = serviceProvider.GetServices<IMailService>();

            // prevent possible enumaration of services IEnumarable
            var mailServices = services as IMailService[] ?? services.ToArray();

            // resolving SendGrid mail service
            sendGridMailService =
                (SendGridMailService)mailServices.First(s => s.GetType() == typeof(SendGridMailService));

            // resolving MailGun mail service
            mailGunMailService = (MailGunMailService)mailServices.First(s => s.GetType() == typeof(MailGunMailService));
        }

        // cosmetic method to show welcome message when user is redirected to the application Url
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Content("Welcome to the backend mail API");
        }

        // the method posting the requests to mail api providers
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] MailModel mail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result;
            try
            {
                try
                {
                    result = await mailGunMailService.Send(mail);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine("There was a problem using primary main API provider. " + exception);
                    result = await sendGridMailService.Send(mail);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine("There was a problem using secondary main API provider. " + exception);
                result = false;
            }

            var message = result ? "The mail has been sent successfully." : "Something went wrong! try again later.";
            var response = new MailResponse { Success = result, Message = message };
            return Ok(response);
        }
    }
}