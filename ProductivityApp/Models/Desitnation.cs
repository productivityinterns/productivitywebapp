using System;
public class Destination {
    public bool zip {set; get;}
    public string emailAddress {set; get;}

    public Destination(string emailAddress, bool zip) {
        this.zip = zip;
        this.emailAddress = emailAddress;    
    }
}