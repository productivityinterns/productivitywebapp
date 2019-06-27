using System;
using System.Collections.Generic;
using ProductivityApp.Controllers;

namespace ProductivityApp.Models
{
    public interface IDatabase
    {
            void DeleteFlow(Guid Id);
        IList<Flow> GetFlows();
        IList<Flow> GetForms();
        IList<Flow> GetTemplates();
        Flow InitializeTemplate(Flow template, IFileHandler fileHandler);
        Flow SaveFlow(FlowController.FillViewModel flow);
        Flow FindFlowById(Guid Id);
        Flow SaveNewTemplate(Flow template);
        Flow StartNewTemplate(Flow template);

        void UpdateFormTemplateAssignments(ViewModels.AssignSubmitViewModel submitFormViewModel);
        int SaveChanges();
    }
}