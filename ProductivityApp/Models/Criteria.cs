using System;
using System.Collections.Generic;

public class Criteria {
    public string name {set; get;}
    public string prompt {set; get;}
    public ICollection<Answer> answers { get; set; }

    //public Answer getAnswer() {
    //    return this.answers;
    //}
    //public void setAnswer(string key, string value) {
    //    this.answers = new Answer(key,value);
    //}

    public Criteria(string name) {
        this.name =  name;
        this.prompt = "Default criteria prompt.";
    } 

}