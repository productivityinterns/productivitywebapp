using System;
public class Form {
    public string name {set; get;} 
    public string path {set; get;}
    public string kind {set; get;}
    public Form()
    {

    }
    public Form(string name, string kind, string path) {
        this.name = name;
        this.kind = kind;
        this.path = path;
    }

}