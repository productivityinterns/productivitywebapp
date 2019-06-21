using System;
using System.Collections.Generic;


[Serializable]
public class Survey {
    public Guid Id { get; set; }
    /// <summary>
    /// Questions shown to the user that have generally free-text answers
    /// </summary>    
    public IList<Field> fields {set; get;}
    public string user {set; get;}
    public DateTime timeCreated {set; get;}
    public string comment{set; get;}
    public Survey()
    {

    }
    
}