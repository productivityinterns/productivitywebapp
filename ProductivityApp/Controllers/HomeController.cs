using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductivityApp.Models;

namespace ProductivityApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Database db = new Database();
            var flows = db.GetTemplates();
            return View(flows);
        }
        /// <summary>
        /// Show all the flows
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
        public IActionResult Flows()
        {

            /*
             * name
               questions
               criteria

              input 1 = name
              question[0].value 

            */
            Database db = new Database();
            var flows = db.GetTemplates();
            return View(flows);
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
