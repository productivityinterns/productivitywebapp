using System;
public class Destination {

    public Guid Id { get; set; }
    public bool zip {set; get;}
    public string emailAddress {set; get;}

    public Destination()
    {

    }

    public Destination(string emailAddress, bool zip) {
        this.zip = zip;
        this.emailAddress = emailAddress;    
    }
}