using Microsoft.AspNetCore.Mvc;

namespace Svendeprøve.Api.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
