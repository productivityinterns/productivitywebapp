using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Serializable]
public class Field {

    /// <summary>
    /// Indicates what sort of input should be shown to the user, and how it should be filtered on the server.
    /// </summary>
    public enum Kinds
    {
    
        String,
        Number,
        Date
    }
    ///<summary>
    /// The type of field: number, string, currency
    ///</summary>
    public Kinds Kind {set; get;}
    /// <summary>
    /// The text shown to the user (the "question")
    /// </summary>
    
    public string prompt {set; get;}
   /// <summary>
   /// A survey-unique short text name of the field. Used in assignment templates.
   /// 
   /// </summary>
   public string tag {set; get;}
   public Guid Id {set; get;}  //string for now, change to guid?    
 
    /// <summary>
    /// The (generally free text) answer given by the user
    /// </summary>
    /// <value></value>
   public string answer {set; get;}
   /// <summary>
   /// If true, the system should remember this user's answers.
   /// Not really used right now!
   /// </summary>   
   public bool remember {set; get;}
   /// <summary>
   /// A filter, that if matching, causes the field to show when prompting the user for input.!--
   /// 
   /// Similarly, if the filter fails to match, the field will hide from the UI when filling out the survey
   /// </summary>   
   public Filter filter {set; get;}

    /// <summary>
    /// The order in which the question should be shown. Lower numbers go first.
    /// Not necessarily unique
    /// </summary>
   public int Order {get;set;}

    public Field()
    {
        
    }
   public Field(Kinds kind, string tag, string prompt, Filter filter, int order = 0){
       this.Kind = kind;
       Id = Guid.NewGuid();
       this.tag = tag;
       this.prompt = prompt;
       this.filter = filter;
       this.Order = order;
   }
}