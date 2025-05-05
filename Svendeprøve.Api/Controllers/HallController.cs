using Microsoft.AspNetCore.Mvc;

namespace Svendeprøve.Api.Controllers
{
    public class HallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
