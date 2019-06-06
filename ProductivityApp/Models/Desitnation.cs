class Destination {
    public bool zip {set; get;}
    public String emailAddress {set; get;}

    public Destination(String emailAddress, bool zip) {
        this.zip = zip;
        this.emailAddress = emailAddress;    
    }
}