using System;
using System.Collections.Generic;

public class Criteria {
    /// <summary>
    /// The name of the group of answers displayed in answers[]
    /// </summary>
    public string Category {set; get;}
    public string prompt {set; get;}
    public List<Answer> answers { get; set; }

    ///<summary>
    ///The selected value among the answers
    ///</summary>
    public string SelectedValue {get;set;}

    //public Answer getAnswer() {
    //    return this.answers;
    //}
    //public void setAnswer(string key, string value) {
    //    this.answers = new Answer(key,value);
    //}
    public Criteria()
    {
        
    }
    public Criteria(string category) {
        this.Category =  category;
        this.prompt = "Default criteria prompt.";
    } 

}