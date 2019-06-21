using System;
/// <summary>
/// An answer is a object that is shown to the user as a possible response to a criteria. 
/// </summary>
[Serializable]
public class Answer {

    public Guid Id { get; set; }
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
   
   /// <summary>
   /// The order in which the answers should be displayed
   /// </summary>   
   public int Order {get;set;}
    public Answer()
    {

    }
    
    public Answer(string text, string value, int order = 0) {
        this.Text = text;
        this.Id = Guid.NewGuid();
        this.value = value;
        this.Order = order;
       }

}