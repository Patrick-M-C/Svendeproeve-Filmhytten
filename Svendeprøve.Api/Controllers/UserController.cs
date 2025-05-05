using Microsoft.AspNetCore.Mvc;

namespace Svendeprøve.Api.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
