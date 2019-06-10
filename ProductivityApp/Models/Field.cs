public class Field {

   public string kind {set; get;}
   public string prompt {set; get;}
   public string id {set; get;}  //string for now, change to guid?
   public string answer {set; get;}
   public bool remember {set; get;}
   public Filter filter {set; get;}


   public Field(string kind, string id, Filter filter){
       this.kind = kind;
       this.id = id;
       this.filter = filter;
   }

}