using System;
using System.Collections.Generic;
[Serializable]
public class Form {
    public string name {set; get;} 
    public string fileName {set; get;}
    public string kind {set; get;}
    public Guid Id {set; get;}
    public IList<Assignment> assignments {set; get;}

    public Form()
    {

    }
    public Form(string name, string kind, string fileName) {
        this.name = name;
        this.kind = kind;
        this.fileName = fileName;
    }

}