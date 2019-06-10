using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityApp.Models
{
    public class Database
    {
        public Flow InitializeTemplate(Flow template)
        {//TODO: Copy all the values from teh template, into a new flow
            
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
                     new Field(Field.Kinds.String,"firstname","Please enter your first name",null),
                     new Field(Field.Kinds.String,"lastname","Please enter your last name",null),
                    
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
