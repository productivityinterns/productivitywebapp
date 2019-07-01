using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.IO.Compression;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;

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

    /// <summary>
    /// This method copies the directory of files from the template into a new directory for the 
    /// instance of the flow.
    /// copying files with directory name of  forms/templateForms/[templateId]
    /// Make destination for forms/activeForms/[destinationId]
    /// into directory name of  forms/activeForms/[destinationId]/forms
    /// <param name="templateId"> The Guid of the template flow. Also the name of the templates directory</param>
    /// <param name="destinationId"> The Guid of the new flow. Also the name of the new flows directory</param>
    /// </summary>
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
        
        var zippable = Path.Combine(destPath,"modified");
        if (!Directory.Exists(zippable)) {
            Directory.CreateDirectory(zippable);
        }
        foreach(string file in Directory.GetFiles(sourcePath)) {
            string dest = Path.Combine(destPath, Path.GetFileName(file));
            File.Copy(file, dest); 
        }        
    }
    public void ClonePlaceHolder(string src, string dest) {
        File.Copy(src,dest);
    }
        ///<summary>
        /// THis function takes a byte array, a file, a guid, and a kind and saves the bytes as a file with
        /// this name and extention given, in a folder in the templates folder with the name of the guid
        ///
        ///</summary>        
        public void SaveForm(Byte[] bytes,string fileName, Guid id, string kind) {
            var templatePath = GetActiveTemplatesPath();
            var templateFolderPath = Path.Combine(templatePath,id.ToString());
            if (!Directory.Exists(templateFolderPath)) {
                Directory.CreateDirectory(templateFolderPath);
            }
            var filePath = Path.Combine(templateFolderPath,(fileName +"."+kind));
            //maybe wrap this in using
            System.IO.File.WriteAllBytes(filePath,bytes);
        }

    ///<summary>
    /// This method iterates through the assignments for each form and prints the user inputed value
    /// to the form field
    /// <param name="flow">The flow containing all assignments and forms for this task.</param>
    ///</summary>
    public void WriteToFiles(Flow flow) {
        string mainPath = GetActiveFormsPath();
        string filePath = Path.Combine(mainPath,flow.Id.ToString(),"forms");

        //iterate through each form
        foreach (Form form in flow.forms)
        {
            PdfDocument pdf = getAPdf(Path.Combine(filePath,form.fileName),
                Path.Combine(filePath,"modified",(form.name+"-filled."+form.kind))  );
            PdfAcroForm acroform = PdfAcroForm.GetAcroForm(pdf, true);
            //then through each assignment
            foreach(Assignment a in form.assignments)
            {
                 //check if assignment's filter is true
                if (flow.checkFilter(a.filter))
                {
                    string theText = flow.GetAssignmentText(a);
                    //Potential problem, for checkboxes that are yes/no they are indexed as 1_01[0], 1_01[1] respectively
                    //may ywant to handle that
                    var formfill = acroform.GetField(a.outputField);
                        if ((formfill == null || formfill is PdfButtonFormField) && theText != "yes" && theText != "true")
                        {
                        } else {
                            acroform.GetField(a.outputField).SetValue(theText);
                        }
                         
                            
                    // This whole method replaces this -> printToDocument(theText,null,GetFormPath(flow,form),"text");
                }
            }
            pdf.Close();
            

        }
    }

    ///<summary>
    /// This method gets the filepath of a specific form in a specific flow
    /// <param name="flow">The flow that contains the form file</param>
    /// <param name="form">The form that contains the file name and type</param>
    /// <returns>path as string</returns>
    ///</summary>
    public  string GetFormPath(Flow flow, Form form)
    {
      return System.IO.Path.Combine(GetActiveFormsPath(),flow.Id.ToString(),"forms",form.fileName);
    }

    ///<summary>
    /// This method gets the filepath of the forms written to by the user
    /// <returns>path as string</returns>
    ///</summary>
    public  string GetActiveFormsPath() {
        return _environment.WebRootFileProvider.GetFileInfo("forms/activeForms").PhysicalPath;
    }
    ///<summary>
    /// This method gets the filepath of the flow template files
    /// <returns>path as string</returns>
    ///</summary>
    public string GetActiveTemplatesPath() {
        return _environment.WebRootFileProvider.GetFileInfo("forms/templateForms").PhysicalPath;
    }

    public string SaveTemplateImage(Byte[] bytes,string fileName, Guid id) {
        var imgFolderPath = GetImagesPath();
        var imagePath = Path.Combine(imgFolderPath, (id.ToString() + ".jpg"));
        System.IO.File.WriteAllBytes(imagePath,bytes);
        return imagePath;
    }

    public string GetImagesPath() {
        return _environment.WebRootFileProvider.GetFileInfo("images/templateImages").PhysicalPath;
    }
    
    ///<summary>
    /// This method creates a zipfile of a directory.
    /// <param name="id">The guid of the current flow, also the name of the flows directory</param>
    /// <returns>path of zipfile as string</returns>
    ///</summary>
    public string Zip(Guid id) {
        //take a flow and zip all the forms
         var fPath = GetActiveFormsPath();
         var filePath = Path.Combine(fPath,id.ToString());
         var formsPath = Path.Combine(filePath,"forms","modified");
         var zipName= id.ToString()+".zip";
         var zipPath = Path.Combine(filePath, zipName);
        if (File.Exists(zipPath)){
            File.Delete(zipPath);
        }
        ZipFile.CreateFromDirectory(formsPath, zipPath);
        return zipPath; 
    }
    ///<summary>
    /// This method delets a specific directory and all of the child directories and files
    /// <param name="id">The guid of  the current flow, also the name of the flows directory</param>
    ///</summary>
    public void DeleteFolder(Guid id, bool isTemplate) {
        var fPath = "";
        if(isTemplate) {
            fPath = GetActiveTemplatesPath();
        } else {
            fPath = GetActiveFormsPath();
        }
        var filePath = Path.Combine(fPath,id.ToString());
        if (Directory.Exists(filePath)) {
            Directory.Delete(filePath,true);//deletes all children
        }
    }
    ///<summary>
    /// This method creates an editable copy of a pdf.
    /// Note: Must call Close() on this  object after all edits are finished.
    /// <param name="filePath">The path as a string of the file that is to be copied and edited</param>
    /// <param name="outputPath">The path of the file that will be created and written to</param>
    /// <returns>PdfDocument object tht can be manipulated.</returns>
    ///</summary>
    public PdfDocument getAPdf(string filePath,string outputPath){
        //PdfReader reader = new PdfReader(path);
        PdfDocument pdf = new PdfDocument(new PdfReader(filePath), new PdfWriter(outputPath));
        return pdf;
        
        // Call this when we send the set off, it makes the pdf non editable
        //form.FlattenFields();
        
    }
}