﻿using Microsoft.AspNetCore.Mvc;

namespace Svendeprøve.Api.Controllers
{
    public class SeatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
