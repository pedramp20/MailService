namespace MailService.Consumer.Controllers
{
    using System.Threading.Tasks;

    using MailService.Consumer.Models;
    using MailService.Consumer.Services;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class MailController : Controller
    {
        private readonly IMailService mailService;

        // mail service is injected
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        // calls the mail service send, which http post the model to the backend api
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] MailModel mail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await mailService.Send(mail);
            return Ok(result);
        }
    }
}