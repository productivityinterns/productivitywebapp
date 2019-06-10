public class Field {

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
   ///<summary>
   /// Short name (slug) without spacing, that is unique
   ///</summary>
   public string id {set; get;}  //string for now, change to guid?
   public string answer {set; get;}
   public bool remember {set; get;}
   public Filter filter {set; get;}

    public Field()
    {
        
    }
   public Field(Kinds kind, string id, string prompt, Filter filter){
       this.Kind = kind;
       this.id = id;
       this.prompt = prompt;
       this.filter = filter;
   }

}