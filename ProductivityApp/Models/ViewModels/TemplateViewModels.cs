using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductivityApp.Models.ViewModels
{
    /// <summary>
    /// A model that contains both fields and criteria for a specific template.
    /// </summary>
    public class TemplateFieldsViewModel
        {
            [Required]
            public Guid Id { get; set; }
            public List<Field> Fields { get; set; }
            public List<Criteria> Criteria { get; set; }
    }
     public class TemplateViewModel
    {
        [Required]
        public string name { get; set; }
        public string description { get; set; }

        public List<Field> fields { get; set; }
        public List<Criteria> criteria { get; set; }
        public List<IFormFile> files { get; set; }

        public IFormFile image {get; set;}
    }

    /// <summary>
    /// Used when the user submits a form from the /Assign page
    /// </summary>
    public class AssignSubmitViewModel
    {
        /// <summary>
        /// The Id of the specific form being edited
        /// </summary>
        public Guid Id { get; set; }
        public List<Assignment> Assignments { get; set; }
    }

}
