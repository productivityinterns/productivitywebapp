﻿using System;

public interface IFileHandler
{    
    void InstantiateDirectory(Guid templateId, Guid destinationId);    
    void WriteToFiles(Flow flow);
    string Zip(Guid id);

    void DeleteFolder(Guid id,bool isTemplate);

    string GetActiveFormsPath();
    string SaveTemplateImage(Byte[] bytes, string fileName, Guid id);
    void SaveForm(Byte[] bytes,string fileName, Guid id, string kind);
}