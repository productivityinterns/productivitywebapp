using System;
using System.Collections.Generic;

[Serializable]
public class Destination {

    public Guid Id { get; set; }
    public bool zip {set; get;}
    public string EmailAddresses {set; get;}

    public Destination()
    {

    }

    public Destination(string emailAddresses, bool zip) {
        this.zip = zip;
        this.EmailAddresses = emailAddresses;    
    }
}