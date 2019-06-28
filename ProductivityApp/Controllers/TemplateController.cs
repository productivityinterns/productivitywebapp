using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductivityApp.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System;
using ProductivityApp.Models.ViewModels;
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

      

        [HttpGet]
        public ActionResult Create()
        {
            return View(new TemplateViewModel ());
        }
        [HttpGet]
        public ActionResult Fields(Guid id)
        {
            var existingTemplate = database.GetTemplates().FirstOrDefault(t => t.Id == id);

            if(existingTemplate == null)
            {
                return NotFound();
            }
            var vm = new TemplateFieldsViewModel
            {
                 Criteria = existingTemplate.criteria.ToList(),
                 Id = existingTemplate.Id,
                 Fields = existingTemplate.inputSurvey.fields.ToList()                                 
            };

            return View(vm);

        }

       


        [HttpPost]
        public ActionResult Fields(TemplateFieldsViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var existingTemplate = database.GetTemplates().FirstOrDefault(t => t.Id == vm.Id);

                if(existingTemplate == null)
                {
                    return NotFound();
                }

                existingTemplate.criteria = vm.Criteria;
                existingTemplate.inputSurvey.fields = vm.Fields;                
                database.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }
        [HttpGet]                
        public ActionResult Assign(Guid id)
        {
            var existingTemplate = database.GetTemplates().FirstOrDefault(t => t.Id == id);
            //TODO: Change this later to the form being edited.
            ViewBag.FormIndex = 0;

            if(existingTemplate == null)
            {
                return NotFound();
            }
            return View(existingTemplate);
        }
     


        [HttpPost]
        public ActionResult Assign(AssignSubmitViewModel vm)
        {
          
            database.UpdateFormTemplateAssignments(vm);

            return Ok();
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
                Flow newTemplate  = database.StartNewTemplate(template);
                newTemplate = database.SaveNewTemplate(template);
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
                if (templateViewModel.image.Name == "image") {
                         
                        using(var readStream = templateViewModel.image.OpenReadStream())
                        {
                            using(var ms = new  MemoryStream())
                            {
                                readStream.CopyTo(ms);
                                var bytes = ms.ToArray();
                                 template.ThumbnailImage = fileHandler.SaveTemplateImage(bytes,Path.GetFileNameWithoutExtension(templateViewModel.image.FileName),template.Id);
                            }
                        }
                        //TODO: FIgure out how the image string is handled then pass it out  that way
                    }
                return RedirectToAction("Fields","Template",new { id = newTemplate.Id } );
            }
            return View(templateViewModel);
        }

    }


}