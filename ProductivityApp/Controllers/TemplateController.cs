using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductivityApp.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProductivityApp.Controllers
{
    public class TemplateController : Controller
    {
        private IDatabase database;
        private IFileHandler fileHandler;
        public TemplateController(IFileHandler fileHandler,IDatabase db)
        {
            
            this.fileHandler = fileHandler;
            this.database = db;
        
        }

        public class TemplateViewModel
        {
            [Required]
            public string name {get;set;}
            public string description {get;set;}

            public List<Field> fields {get;set;}
            public List<Criteria> criteria {get;set;}
            public List<IFormFile> files {get;set;}
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View(new TemplateViewModel ());
        }
        [HttpPost]
        public ActionResult Create(TemplateViewModel templateViewModel)
        {
            
            var files = HttpContext.Request.Form.Files;
            //make sur they filled it out correctly
            if(this.ModelState.IsValid)
            {
                var template = new Flow
                {
                     IsATemplate = true,
                     Description = templateViewModel.description,
                     name = templateViewModel.name,
                     inputSurvey = new Survey
                     {
                          fields = templateViewModel.fields
                     },
                     criteria = templateViewModel.criteria,
                     forms = new List<Form>()
                    
                };
                //for each file that was uploaded, add a form to the template
                foreach(var file in templateViewModel.files)
                {
                   template.forms.Add(new Form
                   {
                        assignments = new List<Assignment>(),
                        fileName = Path.GetFileName(file.FileName),
                        kind = "pdf" ,
                        name = Path.GetFileNameWithoutExtension(file.FileName)
                   });  
                }
                Flow newTemplate  = database.SaveNewTemplate(template);
                 //for each file that was uploaded, copy the file to where  it belongs
                foreach(var file in templateViewModel.files)
                {
                    
                    using(var readStream = file.OpenReadStream())
                    {
                        using(var ms = new  MemoryStream())
                        {
                            readStream.CopyTo(ms);
                            var bytes = ms.ToArray();
                            fileHandler.SaveForm(bytes,Path.GetFileNameWithoutExtension(file.FileName),newTemplate.Id,"pdf");
                        }
                    }
                    
                }
                return RedirectToAction("Index","Flow");
            }
            return View(templateViewModel);
        }

    }
}