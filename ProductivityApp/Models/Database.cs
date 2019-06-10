using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductivityApp.Models
{
    public class Database
    {
        public List<Flow> GetTemplates()
        {//make a sample flow
            Flow flow1 = new Flow{
              name = "Purchase",
              Description = "To buy things.",
              inputSurvey = new Survey
              {
                
              },
              assignments = {},
              criteria = {},
              destinations = {}
      
            };
             Flow flow2 = new Flow{
              name = "Hire",
              Description = "Hire people!",
              inputSurvey = new Survey
              {
                
              },
              assignments = {},
              criteria = {},
              destinations = {}
      
            };
            List<Flow> flows = new List<Flow>();
            flows.Add(flow1);
            flows.Add(flow2);
            
            return flows;
        }
    }
}
