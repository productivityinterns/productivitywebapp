using System;
using System.Collections.Generic;

[Serializable]
public class Criteria {
    public Guid Id { get; set; }
    /// <summary>
    /// The name of the group of answers displayed in answers[]
    /// </summary>
    public string Category {set; get;}
    public string prompt {set; get;}
    /// <summary>
    /// A list of possible answers to present to the user. Only used to show the user the fillable form
    /// </summary>
    public IList<Answer> answers { get; set; }

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