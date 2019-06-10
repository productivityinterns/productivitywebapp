using System;
public class Filter{
    public string name {set; get;}
    public string value {set; get;}

    public Filter(string name, string value) {
        this.name = name;
        this.value = value;
    }

}