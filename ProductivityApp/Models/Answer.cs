using System;
public class Answer {
    /// <summary>
    /// The display name of the answer
    /// </summary>
    public string key {set;get;}
    /// <summary>
    /// The numerical value associated with the answer selected
    /// </summary>
    public string value {set; get;}
    /// <summary>
    /// True if the user has selected this answer for this criteria
    /// </summary>
    public bool Selected { get; set; }

    public Answer(string key, string value) {
        this.key = key;
        this.value = value;
    }

}