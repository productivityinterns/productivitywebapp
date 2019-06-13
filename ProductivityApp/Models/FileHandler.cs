using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Web;
/// <summary>
/// The file handler deals with all forms of reading/writing files (in this case, copying and filling forms)
/// </summary>
public class FileHandler : IFileHandler
{

    IHostingEnvironment _environment;
    public FileHandler(IHostingEnvironment env)
    {
        _environment = env;
    }
//connor
    public void InstantiateDirectory(Guid templateId, Guid destinationId)
    {
        var tPath = GetActiveTemplatesPath();
        var fPath = GetActiveFormsPath();
        var sourcePath = Path.Combine(tPath,templateId.ToString());
        var destPath = Path.Combine(fPath,destinationId.ToString());
        destPath = Path.Combine(destPath,"forms");
        if (!Directory.Exists(destPath)){
            Directory.CreateDirectory(destPath);
        }
        foreach(string file in Directory.GetFiles(sourcePath)) {
            string dest = Path.Combine(destPath, Path.GetFileName(file));
            File.Copy(file, dest);
        }

        Debug.WriteLine(fPath);
            // copying files with directory name of  forms/templateForms/[templateId]
            //Make destination for forms/activeForms/[destinationId]
            // into directory name of  forms/activeForms/[destinationId]
            
    }
    //tyrek
    public void WriteToFiles(Flow flow) {
        //grabs foroms from forms/activeForms/[destinationId]
        //writes criteria/ fields from survey 

        //goes through each form then each assignment in each form checking the filter

        //if checkfilter returns true the action happens
            //assignment calls write and retrieves the text from the field 
            //getText()
            //write it to outputfield of form
            //printToDocument
        //else it doesnt
    }
    //ben
    public void printToDocument(string text,string path, string kind) {

    }

    public string GetActiveFormsPath() {
        return _environment.WebRootFileProvider.GetFileInfo("forms/activeForms").PhysicalPath;
    }
    public string GetActiveTemplatesPath() {
        return _environment.WebRootFileProvider.GetFileInfo("forms/templateForms").PhysicalPath;
    }
    //matt
    public string Zip(Guid id) {
        //take a flow and zip all the forms
         var fPath = GetActiveFormsPath();
         var filePath = Path.Combine(fPath,id.ToString());
         var formsPath = Path.Combine(filePath,"forms");
         var zipName= id.ToString()+".zip";
         var zipPath = Path.Combine(filePath, zipName);
        if (File.Exists(zipPath)){
            File.Delete(zipPath);
        }
        ZipFile.CreateFromDirectory(formsPath, zipPath);
        return zipPath; 
    }
    public void DeleteFolder(Guid id) {
        var fPath = GetActiveFormsPath();
        var filePath = Path.Combine(fPath,id.ToString());
        if (Directory.Exists(filePath)) {
            Directory.Delete(filePath,true);

        }
    }
}