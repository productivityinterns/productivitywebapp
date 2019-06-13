using Microsoft.AspNetCore.Hosting;
using System;
using System.Diagnostics;
using System.IO;
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
        var foo = GetActiveFormsPath();
        Debug.WriteLine(foo);
            // copying files with directory name of  forms/templateForms/[templateId]
            //Make destination for forms/activeForms/[destinationId]
            // into directory name of  forms/activeForms/[destinationId]
            
    }
    //tyrek
    public void WriteToFiles(Flow flow) {
         string mainPath = GetActiveFormsPath();
        string filePath = Path.Combine(mainPath,"placeholder.txt");
        string[] names = new string[] {"yeezus","11037","cyka blyat"};
        
        //iterate through each form
        foreach (Form form in flow.forms)
        {
            foreach(Assignment assignment in form.assignments)
            {
 //then through each assignment
            //check if assignment's filter is true
            if (assignment.filter == null || flow.checkFilter(assignment.filter) == true)
            {
                string theText = flow.GetAssignmentText(assignment);
                printToDocument(theText,assignment.outputField,GetFormPath(flow,form),"text");
            }
            }

        }
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

     public void Write(Flow flow, IFileHandler fileHandler)
    {
       
       
            //if its true, get the text from new func Flow.GetAssignmentText() 
            //we'll stub this out, itl will just return the inputFIeld

            //then, write the text to the doc path in the form
    
/*
        using(StreamWriter sw = new StreamWriter(filePath))
        {
               foreach (string s in names) 
               {
               sw.WriteLine(s);
               }

        }
*/
    }

    public  string GetFormPath(Flow flow, Form form)
    {
      return System.IO.Path.Combine(GetActiveFormsPath(),flow.Id.ToString(),"forms",form.fileName);
    }
    //ben
    //text is what im writing
    //field is where it goes in the doc (e.g. "name" or 0)
    //path is the full path to the file we're writing
    //and kind is the type of file. Right now we have established "text', "pdf"
    public void printToDocument(string text,string field,string path, string kind) {
        string mainPath = GetActiveFormsPath();
        string filePath = Path.Combine(mainPath,"placeholder.txt");
                using(StreamWriter sw = new StreamWriter(filePath))
        {
               
               sw.WriteLine(text);
               

        }

    }

    public  string GetActiveFormsPath() {
        return _environment.WebRootFileProvider.GetFileInfo("forms/activeForms").PhysicalPath;
    }
    public string GetActiveTemplatesPath() {
        return _environment.WebRootFileProvider.GetFileInfo("forms/templateForms").PhysicalPath;
    }
    //matt
    public string Zip(Flow flow) {
        //take a flow and zip all the forms
        //returns path to the zip
        return null; 
    }
}