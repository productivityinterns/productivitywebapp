using System;
/// <summary>
/// An assignment is a particular mapping of a field to a document
/// </summary>
public class Assignment {

    public Guid Id { get; set; }
    private Field inputField;
    private Field outputField;
    private Filter filter;

    public Assignment()
    {

    }
    public Assignment(Field input, Field output, Filter filter) {
        this.inputField = input;
        this.outputField = output;
        this.filter = filter;
    }
    public bool write() {           //returns bool for error handling, maybe replace 
        //write to the pdf if the filter is valid
        if(true) {
            //write to pdf/word/excel
            return true;
        } else {
            return false;
        }
    }

 }