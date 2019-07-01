using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductivityApp.Models;
using System.IO;
namespace ProductivityApp.Controllers
{
    public class FlowController : Controller
    {
        private IDatabase database;
        private IFileHandler fileHandler;
        public FlowController(IFileHandler fileHandler,IDatabase db)
        {
            
            this.fileHandler = fileHandler;
            this.database = db;
        }
        public IActionResult Index()
        {            
            
            var templates = database.GetTemplates();
            var filled = database.GetFlows();
            AllFlows allFlows = new AllFlows() {
                allTemplates = templates,
                allFlows = filled,
            };
            return View(allFlows);
        }

        [HttpGet]
        public IActionResult Fill(Guid id)
        {
           
            var existingFlow = database.GetForms().Where(t=>t.Id == id).FirstOrDefault();
            if(existingFlow == null)
            {
                return NotFound();
            }

          
            return View(existingFlow);
        }

        public class FillViewModel
        {
            public Guid Id { get; set; }
         
            public Survey inputSurvey { set; get; }         
            public IList<Criteria> criteria { set; get; }
            public Destination destination { set; get; }
        }


        [HttpPost]
        public IActionResult Fill(FillViewModel flowVm)
        {
             
            Flow flow = database.SaveFlow(flowVm);
            fileHandler.WriteToFiles(flow);
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

            Flow newFlow = database.InitializeTemplate(existingTemplate,fileHandler);
            return RedirectToAction("fill",new { id = newFlow.Id });
        }
        public IActionResult Remove(Guid id)
        {
            var flow = database.FindFlowById(id);
            database.DeleteFlow(id);
            fileHandler.DeleteFolder(id,flow.IsATemplate); 
            return RedirectToAction("index",null);
        }
        public IActionResult Survey()
        {
            return View();
        }
        public IActionResult Download(Guid id) {
            
            return View(id);
        }

//please forgive me -MG
        public IActionResult DownloadFiles(Guid id)
        {
            string path = fileHandler.Zip(id);
            return File($"~/forms/activeForms/{id.ToString()}/{id.ToString()}.zip", "application/octet-stream",$"forms-{id.ToString()}.zip");
        }

        public IActionResult About() {
            return View();
        }
        [HttpGet]
        public IActionResult Create() {
            return RedirectToAction("create","template");
            //return View(new TemplateViewModel ());
        }
        // public class TemplateViewModel
        // {
        //     public Guid Id { get; set; }
         
        //     public Survey inputSurvey { set; get; }         
        //     public IList<Criteria> criteria { set; get; }
        //     public Destination destination { set; get; }
        //     public  string name {set;get;}
        //     public IList<Form> forms {set; get;}
        //     public  string Description {set;get;}


        // }
        // [HttpPost]
        // public IActionResult Create(TemplateViewModel templateVm) {
        //     //save the template in the template db
        //     //this will redicrect to the pdf assignment page not index.
        //     Debug.Print("FLow name: "+templateVm.name);
        //     Debug.Print("Flow desc: "+templateVm.Description);
        //     Flow template = new Flow {
        //         name = templateVm.name,
        //         Description = templateVm.Description,
        //         IsATemplate = true,
        //         inputSurvey = new Survey(),
                
        //     };
        //     database.SaveNewTemplate(template);
        //     //DO NOT FORGET TO CREATE THE DIRECTORY FOR THE TEMPLATE
        //     //make sure to 
        //     return RedirectToAction("index");
        // }
        public IActionResult DeleteTemplate(Guid id) {
            database.DeleteFlow(id);
            return RedirectToAction("index");
        }
        // public IActionResult Download(string path) {
        //     return File(path, System.Net.Mime.MediaTypeNames.Application.Octet,  Path.GetFileName(path));
        // }
        // public IActionResult Download() {
        //     return View();
        // }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
