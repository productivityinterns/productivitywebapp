using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductivityApp.Controllers;

namespace ProductivityApp.Models
{
    /// <summary>
    /// The Database implements the dbcontext functionality and exposes specific ease-of-use functions for manipulating flows
    /// </summary>
    public class Database : DbContext, IDatabase
    {

        /// <summary>
        /// The list of flows (user instances of templates)
        /// </summary>
        private DbSet<Flow> Flows { get; set; }
     


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=flows.db");
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  
            
            
        }


        /// <summary>
        /// Instantiate a new flow object from the source template.
        /// The new flow object is then saved to a new id, and returned as the function output
        /// </summary>
        /// <param name="template">The source template to copy</param>
        /// <returns>the newly instantiated flow</returns>
        public Flow InitializeTemplate(Flow template)
        {
            //get a copy of flow from the template
            var newFlow = template.initializeFlow();
            
            //add the new flow to the tracked database
            Flows.Add(newFlow);
            //commit changes, must do this!
            SaveChanges();
            return newFlow;
        }
        ///<summary>
        ///This method saves a flow to the DBSet called Flows
        ///</summary>
        public void SaveFlow(FlowController.FillViewModel flow)
        {

            var existingFlow = Flows
                .Include(f=>f.inputSurvey).ThenInclude(f=>f.fields)
                .Include(f=>f.criteria)
                .Include(f=>f.destination).Where(f => f.Id == flow.Id).FirstOrDefault();

            if(existingFlow == null)
            {
                throw new ArgumentException("The specified flow does not exist.");
            }
            //fill in field answers frmo the user
            foreach(var field in existingFlow.inputSurvey.fields)
            {
                var userField = flow.inputSurvey.fields.Where(f => f.Id == field.Id).FirstOrDefault();                
                if(userField != null)
                {
                    field.answer = userField.answer;
                }
                else
                {
                    field.answer = "";
                }
            }
            //Fill in criteria from user input
            foreach(var criteria in existingFlow.criteria)
            {
                var userCriteria = flow.criteria.Where(c => c.Category == criteria.Category).FirstOrDefault();
                if(userCriteria != null)
                {
                    criteria.SelectedValue = userCriteria.SelectedValue;
                }
                else
                {
                    criteria.SelectedValue = null;
                }
            }
            if (flow.destination != null)
            {
                existingFlow.destination.EmailAddresses = flow.destination.EmailAddresses;
                existingFlow.destination.zip = flow.destination.zip;
            }
            
            SaveChanges();

        }
    
         ///This method finds and removes a flow from the DBSet called Flows by identifying a specified GUID
        public void DeleteFlow(Guid Id)
        {
            var flow = Flows.Where(f => f.Id == Id).FirstOrDefault();
            //flow.criteria = new List<Criteria>();
            Flows.Remove(flow);
            SaveChanges();
            
        }
        /// <summary>
        /// Get all forms in the database that are flagged as a template
        /// </summary>
        /// <returns></returns>
        public IList<Flow> GetForms()
        {
            //get all the forms that are not flagged as explicitly a template
            //and include ALL subfields that exist (well, honestly, ones that I remembered!) -mg
            var forms = Flows.Where(t => !t.IsATemplate).Include(t => t.inputSurvey).ThenInclude(t => t.fields)
                .Include(t => t.criteria).ThenInclude(c => c.answers)
                .Include(t => t.destination)
                //.Include(t => t.assignments).ThenInclude(t => t.inputField)
                //.Include(t => t.assignments).ThenInclude(t => t.outputField)
                //.Include(t => t.assignments).ThenInclude(t => t.filter)
                .ToList();
            return forms;
        }
        
        //When we implement GetFlows() we have to filter on !IsATemplate (again, gross hack because we used the same class for both.)
        public IList<Flow> GetTemplates()
        {
            return GetSampleTemplates();
            var templates = Flows.Where(t => t.IsATemplate);
            //get sample flow if none exist
            if(templates.Count() < 3)
            {
                foreach(var template in GetSampleTemplates())
                {
                    Flows.Add(template);
                }
                SaveChanges();
            }
            //This is setup so that I get all the sub-tables required. Sadly we need to do this in EF net core. You will have to do this in GetFlows() as well! -mg
            return Flows.Where(t=>t.IsATemplate).Include(t=>t.inputSurvey).ThenInclude(t=>t.fields)
                .Include(t=>t.criteria).ThenInclude(c=>c.answers)
                .Include(t=>t.destination)
                //.Include(t=>t.assignments).ThenInclude(t=>t.inputField)
                //.Include(t => t.assignments).ThenInclude(t => t.outputField)
                //.Include(t => t.assignments).ThenInclude(t => t.filter)
                .ToList();
        }

        public IList<Flow> GetFlows() {
            SaveChanges();
            var flows = Flows.Where(t=> !t.IsATemplate) ;
            return Flows.Where(t=>!t.IsATemplate).Include(t=>t.inputSurvey).ThenInclude(t=>t.fields)
                .Include(t=>t.criteria).ThenInclude(c=>c.answers)
                .Include(t=>t.destination)
                //.Include(t=>t.assignments).ThenInclude(t=>t.inputField)
                //.Include(t => t.assignments).ThenInclude(t => t.outputField)
                //.Include(t => t.assignments).ThenInclude(t => t.filter)
                .ToList();

        }

        public List<Flow> GetSampleTemplates()
        {//make a sample flow
            Flow template1 = new Flow
            {
                IsATemplate = true,
                name = "Purchase",
                Id = Guid.NewGuid(),
                Description = "To buy things.",
                inputSurvey = new Survey
                {
                    Id = Guid.NewGuid(),
                    fields = new List<Field> {
                     new Field(Field.Kinds.String,"Please enter your first name",null),
                     new Field(Field.Kinds.String,"Please enter your last name",null),
                     new Field(Field.Kinds.String,"Please enter your job title",null),

                }
                },
                forms = new List<Form>(),
               // assignments = new List<Assignment>(),
                criteria = new List<Criteria> {
                  new Criteria{
                       Id = Guid.NewGuid(),
                       prompt = "Credit Card?",
                       Category = "card",
                       answers = new List<Answer>
                       {
                           new Answer("Yes","yes"),
                           new Answer("No","no"),
                           new Answer("Unknown","unknown")

                       }

                  },
                  new Criteria{
                      Id = Guid.NewGuid(),
                       prompt = "Purchase less than $700,000?",
                       Category = "less700k",
                       answers = new List<Answer>
                       {
                           new Answer("Yes","yes"),
                           new Answer("No","no"),
                           new Answer("Unknown","unknown")

                       }

                  },
                new Criteria{
                    Id = Guid.NewGuid(),
                   prompt = "Purchase between $700,000 and $13.5 Million?",
                   Category = "gr100",
                   answers = new List<Answer>
                   {
                       new Answer("Yes","yes"),
                       new Answer("No","no"),
                       new Answer("Unknown","unknown")

                    }
                  }
              },
                destination =  new Destination()
            };
            Flow template2 = new Flow
            {
                IsATemplate = true,
                Id = Guid.NewGuid(),
                name = "Hire",
                Description = "Hire people!",
                forms = new List<Form>(),
                inputSurvey = new Survey
                {
                    Id = Guid.NewGuid(),
                    fields = new List<Field> {
                     new Field(Field.Kinds.String,"Please enter employee first name",null),
                     new Field(Field.Kinds.String,"Please enter employee last name",null),

                }
                },
                //assignments = new List<Assignment>(),
                criteria = new List<Criteria>(),
                destination = new Destination()

            };

            Flow template3 = new Flow
            {
                IsATemplate = true,
                Id = new Guid("5710c736-f5b9-475f-9ef5-76529ea05fb0"),
                name = "Taxes",
                Description = "File your taxes.",                
                inputSurvey = new Survey
                {
                    Id = Guid.NewGuid(),
                    fields = new List<Field> {
                     new Field(Field.Kinds.String,"Please enter employee first name",null),
                     new Field(Field.Kinds.String,"Please enter employee last name",null),

                }
                },
                //assignments = new List<Assignment>(),
                criteria = new List<Criteria>{
                    new Criteria{
                      Id = Guid.NewGuid(),
                       prompt = "Did you provide goods or services in exchange for the vehicle?",
                       Category = "trade",
                       answers = new List<Answer>
                       {
                           new Answer("Yes","yes"),
                           new Answer("No","no"),
                       }

                  },
                },
                destination = new Destination(),
                forms = new List<Form> {
                    new Form {
                        name = "1098-c",
                        fileName = "f1098c.pfd",
                        kind = "pdf"
                        
                    }
                }
            };
            List<Flow> templates = new List<Flow>();
            templates.Add(template1);
            templates.Add(template2);
            templates.Add(template3);

            return templates;
        }
    }
}
