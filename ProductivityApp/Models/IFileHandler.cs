using System;

public interface IFileHandler
{    
    void InstantiateDirectory(Guid templateId, Guid destinationId);    
    void WriteToFiles(Flow flow);
    string Zip(Guid id);

    void DeleteFolder(Guid id);

    string GetActiveFormsPath();
}