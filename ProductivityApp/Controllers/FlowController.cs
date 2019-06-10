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
        private Database database;
        public FlowController()
        {
            database = new Database();
        }
        public IActionResult Index()
        {
        
            var templates = database.GetTemplates();
            return View(templates);
        }

        [HttpGet]
        public IActionResult Fill(string id)
        {
           // var existingFlow = database.GetTemplates().Where(t=>t.name == id).FirstOrDefault();
            var existingFlow = database.GetTemplates().FirstOrDefault();
            if(existingFlow == null)
            {
                return NotFound();
            }
            return View(existingFlow);
        }

        [HttpPost]
        public IActionResult Fill(Flow flow)
        {
            database.SaveFlow(flow);
            return RedirectToAction("Index");
        }
        //initialize takes a template identified by name as specified in id, and creates a new flow based 
        // on that template.
        public IActionResult Initialize(string id)
        {
            var existingTemplate = database.GetTemplates().Where(t=>t.name == id).FirstOrDefault();
            if(existingTemplate == null)
            {
                return NotFound();
            }

            Flow newFlow = database.InitializeTemplate(existingTemplate);
            return RedirectToAction($"fill/{newFlow.Id}");
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
