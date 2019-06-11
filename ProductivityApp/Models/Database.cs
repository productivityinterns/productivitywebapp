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
        {//make a sample flow
            return Templates.ToList();
        }
    }
}
