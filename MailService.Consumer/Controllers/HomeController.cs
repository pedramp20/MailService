namespace MailService.Consumer.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}