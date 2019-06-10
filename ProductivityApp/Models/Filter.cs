using System;
public class Filter{
    public Guid Id { get; set; }
    public string name {set; get;}
    public string value {set; get;}

    public Filter()
    {

    }
    public Filter(string name, string value) {
        this.name = name;
        this.value = value;
    }

}