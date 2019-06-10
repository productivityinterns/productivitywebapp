using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public string prompt {set; get;}
   
   
   public Guid Id {set; get;}  //string for now, change to guid?    
 
   public string answer {set; get;}
   public bool remember {set; get;}
   public Filter filter {set; get;}

    public Field()
    {
        
    }
   public Field(Kinds kind,  string prompt, Filter filter){
       this.Kind = kind;       
   
       this.prompt = prompt;
       this.filter = filter;
   }

}