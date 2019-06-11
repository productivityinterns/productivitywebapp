using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ProductivityApp.Models
{
    /// <summary>
    /// The Database implements the dbcontext functionality and exposes specific ease-of-use functions for manipulating flows
    /// </summary>
    public class Database : DbContext
    {

        /// <summary>
        /// The list of flows (user instances of templates)
        /// </summary>
        private DbSet<Flow> Flows { get; set; }
     


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=flows.db");
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

        public void SaveFlow(Flow flow)
        {
            //TODO: Save something here!
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
                .Include(t => t.destinations)
                .Include(t => t.assignments).ThenInclude(t => t.inputField)
                .Include(t => t.assignments).ThenInclude(t => t.outputField)
                .Include(t => t.assignments).ThenInclude(t => t.filter)
                .ToList();
            return forms;
        }
        
        //When we implement GetFlows() we have to filter on !IsATemplate (again, gross hack because we used the same class for both.)
        public IList<Flow> GetTemplates()
        {
            var templates = Flows.Where(t => t.IsATemplate);
            //get sample flow if none exist
            if(templates.Count() < 2)
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
                .Include(t=>t.destinations)
                .Include(t=>t.assignments).ThenInclude(t=>t.inputField)
                .Include(t => t.assignments).ThenInclude(t => t.outputField)
                .Include(t => t.assignments).ThenInclude(t => t.filter)
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

                }
                },
                assignments = new List<Assignment>(),
                criteria = new List<Criteria> {
                  new Criteria{
                       Id = Guid.NewGuid(),
                       prompt = "Credit Card?",
                       Category = "card",
                       answers = new List<Answer>
                       {
                           new Answer("Yes","yes"),
                           new Answer("No","no"),
                           new Answer("iunno","iunno")

                       }

                  },
                  new Criteria{
                      Id = Guid.NewGuid(),
                       prompt = "Greater Than 100?",
                       Category = "gr100",
                       answers = new List<Answer>
                       {
                           new Answer("Yes","yes"),
                           new Answer("No","no"),
                           new Answer("Unknown","unknown")

                       }

                  }
              },
                destinations = new List<Destination>()
            };
            Flow template2 = new Flow
            {
                IsATemplate = true,
                Id = Guid.NewGuid(),
                name = "Hire",
                Description = "Hire people!",
                inputSurvey = new Survey
                {
                    Id = Guid.NewGuid(),
                    fields = new List<Field> {
                     new Field(Field.Kinds.String,"Please enter employee first name",null),
                     new Field(Field.Kinds.String,"Please enter employee last name",null),

                }
                },
                assignments = new List<Assignment>(),
                criteria = new List<Criteria>(),
                destinations = new List<Destination>()

            };
            List<Flow> templates = new List<Flow>();
            templates.Add(template1);
            templates.Add(template2);

            return templates;
        }
    }
}
