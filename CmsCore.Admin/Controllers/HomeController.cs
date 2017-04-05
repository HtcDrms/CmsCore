using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmsCore.Admin.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace CmsCore.Admin.Controllers
{
    [Authorize(Roles = "ADMIN,HOME")]
    public class HomeController : BaseController
    {
        public HomeController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
