public class Field {

   public String kind {set; get;}
   public String prompt {set; get;}
   public String id {set; get;}  //String for now, change to guid?
   public String answer {set; get;}
   public bool remember {set; get;}
   public Filter filter {set; get;}


   public Field(String kind, String id, Filter filter){
       this.kind = kind;
       this.id = id;
       this.filter = filter;
   }

}