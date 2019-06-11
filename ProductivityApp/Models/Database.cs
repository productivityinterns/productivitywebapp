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
        /// <summary>
        /// The list of templates from which flows are spawned
        /// </summary>
        private DbSet<Flow> Templates { get; set; }


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
        public IList<Flow> GetTemplates()
        {   //get sample flow if none exist
            if(Templates.Count() < 2)
            {
                foreach(var template in GetSampleTemplates())
                {
                    Templates.Add(template);

                }
                SaveChanges();
            }
            return Templates.ToList();
        }


        public List<Flow> GetSampleTemplates()
        {//make a sample flow
            Flow flow1 = new Flow
            {
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
                assignments = { },
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
                destinations = { }

            };
            Flow flow2 = new Flow
            {
                Id = Guid.NewGuid(),
                name = "Hire",
                Description = "Hire people!",
                inputSurvey = new Survey
                {

                },
                assignments = { },
                criteria = new List<Criteria>(),
                destinations = { }

            };
            List<Flow> flows = new List<Flow>();
            flows.Add(flow1);
            flows.Add(flow2);

            return flows;
        }
    }
}
