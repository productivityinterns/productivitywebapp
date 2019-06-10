using System;
public class Answer {
    /// <summary>
    /// The display name of the answer
    /// </summary>
    public string Text {set;get;}
    /// <summary>
    /// The numerical value associated with the answer selected
    /// </summary>
    public string value {set; get;}
    /// <summary>
    /// True if the user has selected this answer for this criteria
    /// </summary>
   

    
    public Answer(string text, string value) {
        this.Text = text;
        this.value = value;
       }

}