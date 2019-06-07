public class Form {
    public String name {set; get;} 
    public String path {set; get;}
    public String kind {set; get;}

    public Form(String name, String kind, String path) {
        this.name = name;
        this.kind = kind;
        this.path = path;
    }

}