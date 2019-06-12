using System;
public class FileHandler {

//connor
    public void InstantiateDirectory(Guid templateId, Guid destinationId) {
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
        return null;
    }
    public string GetActiveTemplatesPath() {
        return null;
    }
    //matt
    public string zip(Flow flow) {
        //take a flow and zip all the forms
        //returns path to the zip
        return null; 
    }
}