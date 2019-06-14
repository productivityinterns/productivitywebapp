using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Collections.Generic;
using System.Web;
using System.IO;
using iText;
using iText.Forms;
using iText.Forms.Fields;
using iText.Pdfa;
using iText.IO.Colors;
using iText.Signatures;
using iText.Kernel;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Layer;

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
         string mainPath = GetActiveFormsPath();
        string filePath = Path.Combine(mainPath,flow.Id.ToString(),"forms");
        string[] names = new string[] {"yeezus","11037","cyka blyat"};

        //iterate through each form
        foreach (Form form in flow.forms)
        {
            foreach(Field f in flow.inputSurvey.fields)
            {
 //then through each assignment
            //check if assignment's filter is true
            if (f.filter != null && flow.checkFilter(f.filter))
            {
                //string theText = flow.GetAssignmentText(f.answer);
               // printToDocument(theText,null,GetFormPath(flow,form),"text");
            }
            }
             getAPdf(Path.Combine(filePath,"f1098c.pdf"),Path.Combine(filePath,"newFile.pdf"),flow.inputSurvey.fields);

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
        
                using(StreamWriter sw = new StreamWriter(path,true))
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
    public void getAPdf(string filePath,string outputPath, IList<Field> list){
        //PdfReader reader = new PdfReader(path);
        PdfDocument pdf = new PdfDocument(new PdfReader(filePath), new PdfWriter(outputPath));
        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
        IDictionary<String, PdfFormField> fields = form.GetFormFields();
        PdfFormField currentField;
        int i = 1;
        foreach(Field item in list) {
            try{
                string value = "topmostSubform[0].CopyA[0].TopLeftColumn[0].f1_"+i.ToString()+"[0]";
                fields.TryGetValue(String.Format(value), out currentField);
                currentField.SetValue(item.answer);
            i++;

        } catch(NullReferenceException e) {
            Debug.Print(e.Message);
        }
        }
        
        form.FlattenFields();
        pdf.Close();
        
    }
}