﻿using Microsoft.AspNetCore.Mvc;

namespace Courses.WebUI.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult Enrolled()
        {
            return View();
        }
    }
}
