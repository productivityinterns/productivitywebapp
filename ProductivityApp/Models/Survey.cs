using System;
using System.Collections.Generic;
public class Survey {
    public Guid Id { get; set; }
    public IList<Field> fields {set; get;}
    public string user {set; get;}
    public DateTime timeCreated {set; get;}
    public string comment{set; get;}
    public Survey()
    {

    }
    public Survey(string user) {
        this.user = user;
        this.timeCreated = DateTime.Now; //Date time in local time, maybe standardize in future
    }

}