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


        private DbSet<Flow> Flows { get; set; }
        /// <summary>
        /// 
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
            var newFlow = template.initializeFlow();

            return template;
        }

        public void SaveFlow(Flow flow)
        {
            //TODO: Save something here!
        }
        public List<Flow> GetTemplates()
        {//make a sample flow
            Flow flow1 = new Flow{
              name = "Purchase",
              Description = "To buy things.",
              inputSurvey = new Survey
              {
                fields = new List<Field> {
                     new Field(Field.Kinds.String,"Please enter your first name",null),
                     new Field(Field.Kinds.String,"Please enter your last name",null),
                    
                }
              },
              assignments = {},
              criteria = new List<Criteria> {
                  new Criteria{
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
              destinations = {}
      
            };
             Flow flow2 = new Flow{
              name = "Hire",
              Description = "Hire people!",
              inputSurvey = new Survey
              {
                
              },
              assignments = {},
              criteria = new List<Criteria>(),
              destinations = {}
      
            };
            List<Flow> flows = new List<Flow>();
            flows.Add(flow1);
            flows.Add(flow2);
            
            return flows;
        }
    }
}
