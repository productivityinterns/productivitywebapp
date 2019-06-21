using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Serializable]
public class Criteria {
    public Guid Id { get; set; }

    /// <summary>
    /// The order the criteria should be displayed to the user.
    /// </summary>    
    public int Order {get;set;}
    /// <summary>
    /// The name of the group of answers displayed in answers[]
    /// Use this as a tag when doing an assignement
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

    //MG -- you should use generic copy funcs here to avoid 'forgetting' newly added fields later.    
    public void copyCriteriaInto(Criteria destination){
        destination.Id = this.Id;
        destination.Category = this.Category;
        destination.answers = this.answers;
        destination.prompt = this.prompt;
        destination.SelectedValue = this.SelectedValue;
        destination.Order = this.Order;
    }

    public Criteria()
    {
        
    }
    public Criteria(string category,int order = 0) {
        this.Category =  category;
        this.prompt = "Default criteria prompt.";
        this.Order = order;
    } 

}