class Survey {
    public Field[] fields {set; get;}
    public String user {set; get;}
    public DateTime timeCreated {set; get;}
    public String comment{set; get;}

    public Survey(String user) {
        this.user = user;
        this.timeCreated = DateTime.Now; //Date time in local time, maybe standardize in future
    }

}