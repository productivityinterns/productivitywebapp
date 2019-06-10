using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductivityApp.Models;

namespace ProductivityApp.Controllers
{
    public class FlowController : Controller
    {
        public IActionResult Index()
        {
            var database = new Database();
            var templates = database.GetTemplates();
            return View(templates);
        }
        public IActionResult Survey()
        {
            return View();
        }
        public IActionResult Download() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
