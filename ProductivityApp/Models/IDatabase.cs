﻿using System;
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
        void SaveFlow(FlowController.FillViewModel flow);
    }
}